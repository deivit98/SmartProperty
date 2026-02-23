namespace SmartProperty.Kafka;

/// <summary>
/// Standard key for all Kafka messages: operation id, entity type, and UTC timestamp.
/// </summary>
/// <param name="Operation">Operation/correlation id.</param>
/// <param name="EntityType">Entity the message is about.</param>
/// <param name="Date">UTC timestamp when the key was created.</param>
public record MessageKey(Guid Operation, MessageEntityType EntityType, DateTime Date)
{
    /// <summary>
    /// Creates a new message key with a new operation id and current UTC time.
    /// </summary>
    public static MessageKey New(MessageEntityType entityType) =>
        new(Guid.NewGuid(), entityType, DateTime.UtcNow);
}
