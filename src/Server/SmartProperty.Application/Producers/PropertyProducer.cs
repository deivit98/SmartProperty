using Confluent.Kafka;
using SmartProperty.Kafka;

namespace SmartProperty.Application.Producers;

/// <summary>
/// Kafka producer for property-created events; uses MessageKey and KafkaTopics.PropertyCreated.
/// </summary>
public sealed class PropertyProducer(IProducer<MessageKey, PropertyCreatedMessage> producer) : IPropertyProducer
{
    private readonly IProducer<MessageKey, PropertyCreatedMessage> _producer = producer;

    /// <inheritdoc />
    public async Task PublishAsync(PropertyCreatedMessage message, CancellationToken cancellationToken = default)
    {
        var key = MessageKey.New(MessageEntityType.Property);
        await _producer.ProduceAsync(
            KafkaTopics.PropertyCreated,
            new Message<MessageKey, PropertyCreatedMessage> { Key = key, Value = message },
            cancellationToken);
    }
}
