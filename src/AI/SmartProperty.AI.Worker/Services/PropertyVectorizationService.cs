using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using SmartProperty.AI.Worker.Models;
using SmartProperty.Kafka;
using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace SmartProperty.AI.Worker.Services;

/// <summary>
/// Vectorizes property details using Ollama embeddings and stores/retrieves them in Qdrant.
/// </summary>
public sealed class PropertyVectorizationService(
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
    QdrantClient qdrantClient,
    ILogger<PropertyVectorizationService> logger) : IPropertyVectorizationService
{
    private const string CollectionName = "properties";
    private const uint VectorSize = 384; // all-minilm embedding dimension

    public async Task VectorizeAndStoreAsync(PropertyCreatedMessage message, CancellationToken cancellationToken = default)
    {
        var text = BuildSearchableText(message);
        if (string.IsNullOrWhiteSpace(text))
        {
            logger.LogWarning("Property {PropertyId} has no searchable text; skipping vectorization", message.Id);
            return;
        }

        await EnsureCollectionExistsAsync(cancellationToken).ConfigureAwait(false);

        var generated = await embeddingGenerator.GenerateAsync([text], default, cancellationToken).ConfigureAwait(false);
        var embedding = generated.Count > 0 ? generated[0] : null;
        if (embedding is null)
        {
            logger.LogWarning("No embedding generated for property {PropertyId}", message.Id);
            return;
        }

        var vectorFloats = embedding.Vector.ToArray();
        var point = new PointStruct
        {
            Id = message.Id,
            Vectors = vectorFloats,
            Payload =
            {
                ["title"] = message.Title ?? "",
                ["description"] = message.Description ?? "",
                ["createdAt"] = message.CreatedAt.Ticks
            }
        };

        await qdrantClient.UpsertAsync(CollectionName, [point], wait: true, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        logger.LogInformation("Vectorized and stored property {PropertyId} in Qdrant", message.Id);
    }

    public async Task<PropertySearchMatch?> SearchAsync(string query, int limit = 1, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return null;

        var generated = await embeddingGenerator.GenerateAsync([query], default, cancellationToken).ConfigureAwait(false);
        var embedding = generated.Count > 0 ? generated[0] : null;
        if (embedding is null)
            return null;

        var exists = await qdrantClient.CollectionExistsAsync(CollectionName, cancellationToken).ConfigureAwait(false);
        if (!exists)
            return null;

        var results = await qdrantClient.SearchAsync(
            CollectionName,
            embedding.Vector,
            filter: null,
            searchParams: null,
            limit: (ulong)limit,
            offset: 0,
            payloadSelector: null,
            vectorsSelector: null,
            scoreThreshold: null,
            vectorName: null,
            readConsistency: null,
            shardKeySelector: null,
            sparseIndices: null,
            timeout: null,
            cancellationToken: cancellationToken).ConfigureAwait(false);

        var first = results.FirstOrDefault();
        if (first is null)
            return null;

        return ScoredPointToMatch(first);
    }

    private static string BuildSearchableText(PropertyCreatedMessage message)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(message.Title))
            parts.Add(message.Title.Trim());
        if (!string.IsNullOrWhiteSpace(message.Description))
            parts.Add(message.Description.Trim());
        return string.Join(" ", parts);
    }

    private async Task EnsureCollectionExistsAsync(CancellationToken cancellationToken)
    {
        var exists = await qdrantClient.CollectionExistsAsync(CollectionName, cancellationToken).ConfigureAwait(false);
        if (exists)
            return;

        await qdrantClient.CreateCollectionAsync(
            CollectionName,
            new VectorParams { Size = VectorSize, Distance = Distance.Cosine },
            shardNumber: 1,
            replicationFactor: 1,
            writeConsistencyFactor: 1,
            onDiskPayload: false,
            hnswConfig: null,
            optimizersConfig: null,
            walConfig: null,
            quantizationConfig: null,
            initFromCollection: null,
            shardingMethod: null,
            sparseVectorsConfig: null,
            strictModeConfig: null,
            timeout: null,
            cancellationToken: cancellationToken).ConfigureAwait(false);
        logger.LogInformation("Created Qdrant collection {CollectionName} with vector size {VectorSize}", CollectionName, VectorSize);
    }

    private static PropertySearchMatch? ScoredPointToMatch(ScoredPoint point)
    {
        if (point.Id?.Uuid is null or "")
            return null;

        var id = Guid.TryParse(point.Id.Uuid.ToString(), out var guid) ? guid : Guid.Empty;
        point.Payload.TryGetValue("title", out var titleVal);
        point.Payload.TryGetValue("description", out var descVal);
        point.Payload.TryGetValue("createdAt", out var createdAtVal);

        var title = titleVal?.StringValue ?? "";
        var description = descVal?.KindCase == Value.KindOneofCase.StringValue ? descVal.StringValue : null;
        var createdAt = createdAtVal?.KindCase == Value.KindOneofCase.IntegerValue
            ? new DateTime(createdAtVal.IntegerValue, DateTimeKind.Utc)
            : DateTime.MinValue;

        var score = point.Score;
        return new PropertySearchMatch(id, title, description, createdAt, score);
    }
}
