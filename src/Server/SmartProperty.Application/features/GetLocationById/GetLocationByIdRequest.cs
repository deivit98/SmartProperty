using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.GetLocationById;

public readonly struct GetLocationByIdRequest : IRequest<Result<GetLocationByIdResponse>>
{
    public Guid Id { get; }

    public GetLocationByIdRequest(Guid id)
    {
        Id = id;
    }
}
