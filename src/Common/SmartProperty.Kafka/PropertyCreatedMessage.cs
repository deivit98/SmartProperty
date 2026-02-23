namespace SmartProperty.Kafka;

/// <summary>
/// Event payload when a property is created; shared contract for API producer and AI.Web consumer.
/// </summary>
/// <param name="Id">Property id.</param>
/// <param name="Title">Property title.</param>
/// <param name="Description">Optional description.</param>
/// <param name="CreatedAt">UTC creation time.</param>
public record PropertyCreatedMessage(Guid Id, string Title, string? Description, DateTime CreatedAt);
