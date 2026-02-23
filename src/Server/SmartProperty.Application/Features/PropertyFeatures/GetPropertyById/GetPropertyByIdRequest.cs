using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.GetPropertyById;

public readonly struct GetPropertyByIdRequest(Guid id) : IRequest<Result<GetPropertyByIdResponse>>
{
    public Guid Id { get; } = id;
}
