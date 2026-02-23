using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.GetSellerById;

public readonly struct GetSellerByIdRequest(Guid id) : IRequest<Result<GetSellerByIdResponse>>
{
    public Guid Id { get; } = id;
}
