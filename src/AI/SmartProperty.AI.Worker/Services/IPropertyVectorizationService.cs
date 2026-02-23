using SmartProperty.AI.Worker.Models;
using SmartProperty.Kafka;

namespace SmartProperty.AI.Worker.Services;

/// <summary>
/// Vectorizes property details and stores them in Qdrant; supports semantic search.
/// </summary>
public interface IPropertyVectorizationService
{
    /// <summary>
    /// Builds searchable text from the message, generates an embedding via Ollama, and upserts the point into Qdrant.
    /// </summary>
    Task VectorizeAndStoreAsync(PropertyCreatedMessage message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Embeds the query and returns the single best-matching property, or null if none.
    /// </summary>
    Task<PropertySearchMatch?> SearchAsync(string query, int limit = 1, CancellationToken cancellationToken = default);
}
