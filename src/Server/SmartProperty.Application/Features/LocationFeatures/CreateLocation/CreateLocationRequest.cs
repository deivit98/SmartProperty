using MediatR;
using SmartProperty.Application.Features.LocationFeatures.Models;
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
