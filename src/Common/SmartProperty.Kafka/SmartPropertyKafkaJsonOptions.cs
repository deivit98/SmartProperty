using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartProperty.Kafka;

/// <summary>
/// Shared JSON options for Kafka key and value serialization.
/// </summary>
public static class SmartPropertyKafkaJsonOptions
{
    /// <summary>
    /// Default options used by message key and value serializers (case-insensitive property names).
    /// </summary>
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
}
