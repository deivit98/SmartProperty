using MediatR;
using SmartProperty.Domain.Entities;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.GetLocationById;

public sealed class GetLocationByIdHandler : IRequestHandler<GetLocationByIdRequest, Result<GetLocationByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetLocationByIdHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetLocationByIdResponse>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
    {
        var location = await _dbContext.GetLocationByID(request.Id, cancellationToken);

        if (location is null)
            return Result<GetLocationByIdResponse>.Fail(ErrorCode.NotFound, "Location not found.");

        var response = MapToResponse(location);
        return Result<GetLocationByIdResponse>.Ok(response);
    }

    private static GetLocationByIdResponse MapToResponse(Location location)
    {
        return new GetLocationByIdResponse(
            location.Id,
            location.Address,
            location.City,
            location.State,
            location.Country,
            location.ZipCode,
            location.Latitude,
            location.Longitude,
            location.Neighborhood);
    }
}
