using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.GetLocationById;

public readonly struct GetLocationByIdRequest(Guid id) : IRequest<Result<GetLocationByIdResponse>>
{
    public Guid Id { get; } = id;
}
