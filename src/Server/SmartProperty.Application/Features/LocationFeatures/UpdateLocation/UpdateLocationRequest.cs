using MediatR;
using SmartProperty.Application.Features.LocationFeatures.CreateLocation;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.UpdateLocation;

/// <summary>
/// Request for updating a location. Id comes from the route; body is the same shape as <see cref="CreateLocationRequest"/>.
/// </summary>
public record UpdateLocationRequest(Guid Id, CreateLocationRequest Body) : IRequest<Result<UpdateLocationResponse>>;
