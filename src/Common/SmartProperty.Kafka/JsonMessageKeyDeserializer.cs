using System.Text.Json;
using Confluent.Kafka;

namespace SmartProperty.Kafka;

/// <summary>
/// Confluent.Kafka deserializer for <see cref="MessageKey"/> using System.Text.Json.
/// </summary>
public sealed class JsonMessageKeyDeserializer(JsonSerializerOptions? options = null) : IDeserializer<MessageKey>
{
    private readonly JsonSerializerOptions? _options = options ?? SmartPropertyKafkaJsonOptions.Default;

    /// <inheritdoc />
    public MessageKey Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty)
            return null!;
        return JsonSerializer.Deserialize<MessageKey>(data, _options)!;
    }
}
