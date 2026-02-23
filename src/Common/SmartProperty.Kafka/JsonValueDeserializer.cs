using System.Text.Json;
using Confluent.Kafka;

namespace SmartProperty.Kafka;

/// <summary>
/// Confluent.Kafka deserializer for message values using System.Text.Json.
/// </summary>
public sealed class JsonValueDeserializer<T>(JsonSerializerOptions? options = null) : IDeserializer<T>
{
    private readonly JsonSerializerOptions? _options = options ?? SmartPropertyKafkaJsonOptions.Default;

    /// <inheritdoc />
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull || data.IsEmpty)
            return default!;
        return JsonSerializer.Deserialize<T>(data, _options)!;
    }
}
