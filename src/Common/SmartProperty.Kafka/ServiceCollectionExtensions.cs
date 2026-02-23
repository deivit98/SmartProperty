using Aspire.Confluent.Kafka;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace SmartProperty.Kafka;

/// <summary>
/// Extension methods to register SmartProperty Kafka producers and consumers with <see cref="MessageKey"/> and JSON serialization.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a Kafka producer with <see cref="MessageKey"/> as key type and JSON serialization for key and value.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="connectionName">Connection name (default "kafka").</param>
    public static void AddSmartPropertyKafkaProducer<TValue>(this IHostApplicationBuilder builder, string connectionName = "kafka")
    {
        builder.AddKafkaProducer<MessageKey, TValue>(connectionName, producerBuilder =>
        {
            producerBuilder.SetKeySerializer(new JsonMessageKeySerializer());
            producerBuilder.SetValueSerializer(new JsonValueSerializer<TValue>());
        });
    }

    /// <summary>
    /// Registers a Kafka consumer with <see cref="MessageKey"/> as key type and JSON deserialization for key and value.
    /// </summary>
    /// <param name="builder">The host application builder.</param>
    /// <param name="connectionName">Connection name (default "kafka").</param>
    /// <param name="groupId">Consumer group id (required by Kafka).</param>
    /// <param name="configureSettings">Optional consumer settings configuration.</param>
    /// <param name="configureConsumerBuilder">Optional consumer builder configuration (e.g. additional Confluent.Kafka options).</param>
    public static void AddSmartPropertyKafkaConsumer<TValue>(
        this IHostApplicationBuilder builder,
        string connectionName,
        string groupId,
        Action<KafkaConsumerSettings>? configureSettings = null,
        Action<ConsumerBuilder<MessageKey, TValue>>? configureConsumerBuilder = null)
    {
        builder.AddKafkaConsumer<MessageKey, TValue>(
            connectionName,
            settings =>
            {
                settings.Config.GroupId = groupId;
                configureSettings?.Invoke(settings);
            },
            consumerBuilder =>
            {
                consumerBuilder.SetKeyDeserializer(new JsonMessageKeyDeserializer());
                consumerBuilder.SetValueDeserializer(new JsonValueDeserializer<TValue>());
                configureConsumerBuilder?.Invoke(consumerBuilder);
            });
    }
}
