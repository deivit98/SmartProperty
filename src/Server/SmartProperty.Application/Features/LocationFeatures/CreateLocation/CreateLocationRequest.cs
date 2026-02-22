using MediatR;
using SmartProperty.Application.Features.Models;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.CreateLocation;

/// <summary>
/// Request body for creating a location. Bind directly from POST body.
/// </summary>
public record CreateLocationRequest(
    string Address,
    string City,
    string State,
    string Country,
    string? ZipCode = null,
    double? Latitude = null,
    double? Longitude = null,
    string? Neighborhood = null
) : IRequest<Result<CreateLocationResponse>>;

public static class CreateLocationRequestExtensions
{
    public static LocationModel ToModel(this CreateLocationRequest request)
    {
        return new LocationModel
        {
            Id = Guid.Empty,
            Address = request.Address,
            City = request.City,
            State = request.State,
            Country = request.Country,
            ZipCode = request.ZipCode,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Neighborhood = request.Neighborhood
        };
    }
}
