using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartProperty.Kafka;

namespace SmartProperty.AI.Worker.Consumers;

/// <summary>
/// Background consumer for property-created events from Kafka.
/// </summary>
public sealed class PropertyCreatedConsumer(
    IConsumer<MessageKey, PropertyCreatedMessage> consumer,
    ILogger<PropertyCreatedConsumer> logger) : BackgroundService
{
    private readonly IConsumer<MessageKey, PropertyCreatedMessage> _consumer = consumer;
    private readonly ILogger<PropertyCreatedConsumer> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(KafkaTopics.PropertyCreated);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    if (result?.Message?.Value is { } message)
                        await ProcessAsync(message, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming property-created message");
                }
            }
        }
        finally
        {
            _consumer.Close();
        }

        await Task.CompletedTask;
    }

    private Task ProcessAsync(PropertyCreatedMessage message, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Property created: Id={PropertyId}, Title={Title}, CreatedAt={CreatedAt}",
            message.Id,
            message.Title,
            message.CreatedAt);

        // TODO: e.g. trigger embeddings, index in Qdrant, etc.
        return Task.CompletedTask;
    }
}
