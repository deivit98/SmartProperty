using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.DeleteProperty;

public readonly struct DeletePropertyRequest(Guid id) : IRequest<Result<Unit>>
{
    public Guid Id { get; } = id;
}
