using MediatR;
using SmartProperty.Application.Features.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.CreateLocation;

public class CreateLocationHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateLocationRequest, Result<CreateLocationResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<CreateLocationResponse>> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Address))
            return Result<CreateLocationResponse>.Fail(ErrorCode.EmptyParameter, "Address is required.");
        if (string.IsNullOrWhiteSpace(request.City))
            return Result<CreateLocationResponse>.Fail(ErrorCode.EmptyParameter, "City is required.");
        if (string.IsNullOrWhiteSpace(request.Country))
            return Result<CreateLocationResponse>.Fail(ErrorCode.EmptyParameter, "Country is required.");

        var location = request.ToModel().ToEntity(Guid.NewGuid());
        await _dbContext.AddLocationAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<CreateLocationResponse>.Ok(new CreateLocationResponse(location.ToModel()));
    }
}
