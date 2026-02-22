using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.DeleteLocation;

public readonly struct DeleteLocationRequest(Guid id) : IRequest<Result<Unit>>
{
    public Guid Id { get; } = id;
}
