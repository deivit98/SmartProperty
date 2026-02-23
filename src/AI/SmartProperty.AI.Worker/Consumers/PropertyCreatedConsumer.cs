using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartProperty.AI.Worker.Services;
using SmartProperty.Kafka;

namespace SmartProperty.AI.Worker.Consumers;

/// <summary>
/// Background consumer for property-created events from Kafka.
/// </summary>
public sealed class PropertyCreatedConsumer(
    IConsumer<MessageKey, PropertyCreatedMessage> consumer,
    IServiceScopeFactory scopeFactory,
    ILogger<PropertyCreatedConsumer> logger) : BackgroundService
{
    private readonly IConsumer<MessageKey, PropertyCreatedMessage> _consumer = consumer;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
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

    private async Task ProcessAsync(PropertyCreatedMessage message, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Property created: Id={PropertyId}, Title={Title}, CreatedAt={CreatedAt}",
            message.Id,
            message.Title,
            message.CreatedAt);

        await using var scope = _scopeFactory.CreateAsyncScope();
        var vectorizationService = scope.ServiceProvider.GetRequiredService<IPropertyVectorizationService>();
        await vectorizationService.VectorizeAndStoreAsync(message, cancellationToken);
    }
}
