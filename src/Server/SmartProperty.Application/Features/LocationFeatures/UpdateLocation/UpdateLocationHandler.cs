using MediatR;
using SmartProperty.Application.Features.LocationFeatures.CreateLocation;
using SmartProperty.Application.Features.LocationFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.UpdateLocation;

public class UpdateLocationHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateLocationRequest, Result<UpdateLocationResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<UpdateLocationResponse>> Handle(UpdateLocationRequest request, CancellationToken cancellationToken)
    {
        var location = await _dbContext.GetTrackedLocationByID(request.Id, cancellationToken);
        if (location is null)
            return Result<UpdateLocationResponse>.Fail(ErrorCode.NotFound, "Location not found.");

        request.Body.ToModel().ApplyTo(location);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<UpdateLocationResponse>.Ok(new UpdateLocationResponse(location.ToModel()));
    }
}
