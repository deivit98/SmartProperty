namespace SmartProperty.Kafka;

/// <summary>
/// Entity type for Kafka message keys; aligns with domain entities that produce or consume events.
/// </summary>
public enum MessageEntityType
{
    Property,
    Location,
    Seller,
}
