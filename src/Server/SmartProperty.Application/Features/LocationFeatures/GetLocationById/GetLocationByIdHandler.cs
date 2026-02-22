using MediatR;
using SmartProperty.Application.Features.Models;
using SmartProperty.Domain.Entities;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.GetLocationById;

public sealed class GetLocationByIdHandler(ApplicationDbContext dbContext) : IRequestHandler<GetLocationByIdRequest, Result<GetLocationByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetLocationByIdResponse>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
    {
        var location = await _dbContext.GetLocationByID(request.Id, cancellationToken);

        if (location is null)
            return Result<GetLocationByIdResponse>.Fail(ErrorCode.NotFound, "Location not found.");

        var response = new GetLocationByIdResponse(MapToModel(location));
        return Result<GetLocationByIdResponse>.Ok(response);
    }

    private static LocationModel MapToModel(Location location)
    {
        return new LocationModel
        {
            Id = location.Id,
            Address = location.Address,
            City = location.City,
            State = location.State,
            Country = location.Country,
            ZipCode = location.ZipCode,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Neighborhood = location.Neighborhood
        };
    }
}
