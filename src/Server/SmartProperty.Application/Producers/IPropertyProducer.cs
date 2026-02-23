using SmartProperty.Kafka;

namespace SmartProperty.Application.Producers;

/// <summary>
/// Publishes property-created events to Kafka; hides the generic producer from the application layer.
/// </summary>
public interface IPropertyProducer
{
    /// <summary>
    /// Publishes a property-created message.
    /// </summary>
    Task PublishAsync(PropertyCreatedMessage message, CancellationToken cancellationToken = default);
}
