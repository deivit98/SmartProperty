using System.Text.Json;
using Confluent.Kafka;

namespace SmartProperty.Kafka;

/// <summary>
/// Confluent.Kafka serializer for message values using System.Text.Json.
/// </summary>
public sealed class JsonValueSerializer<T>(JsonSerializerOptions? options = null) : ISerializer<T>
{
    private readonly JsonSerializerOptions? _options = options ?? SmartPropertyKafkaJsonOptions.Default;

    /// <inheritdoc />
    public byte[] Serialize(T data, SerializationContext context)
    {
        if (data == null!)
            return null!;
        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}
