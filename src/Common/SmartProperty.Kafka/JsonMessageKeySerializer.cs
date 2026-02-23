using System.Text.Json;
using Confluent.Kafka;

namespace SmartProperty.Kafka;

/// <summary>
/// Confluent.Kafka serializer for <see cref="MessageKey"/> using System.Text.Json.
/// </summary>
public sealed class JsonMessageKeySerializer(JsonSerializerOptions? options = null) : ISerializer<MessageKey>
{
    private readonly JsonSerializerOptions? _options = options ?? SmartPropertyKafkaJsonOptions.Default;

    /// <inheritdoc />
    public byte[] Serialize(MessageKey data, SerializationContext context)
    {
        if (data == null!)
            return null!;
        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}
