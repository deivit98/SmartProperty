using MediatR;
using SmartProperty.Application.Features.PropertyFeatures.Models;
using SmartProperty.Application.Producers;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.PropertyExtensions.Write;
using SmartProperty.Kafka;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.CreateProperty;

public class CreatePropertyHandler(ApplicationDbContext dbContext, IPropertyProducer propertyProducer) : IRequestHandler<CreatePropertyRequest, Result<CreatePropertyResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPropertyProducer _propertyProducer = propertyProducer;

    public async Task<Result<CreatePropertyResponse>> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return Result<CreatePropertyResponse>.Fail(ErrorCode.EmptyParameter, "Title is required.");

        var property = request.ToEntity(Guid.NewGuid());
        await _dbContext.AddPropertyAsync(property, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var message = new PropertyCreatedMessage(property.Id, property.Title, property.Description, property.CreatedAt);
        await _propertyProducer.PublishAsync(message, cancellationToken);

        return Result<CreatePropertyResponse>.Ok(new CreatePropertyResponse(property.ToModel()));
    }
}
