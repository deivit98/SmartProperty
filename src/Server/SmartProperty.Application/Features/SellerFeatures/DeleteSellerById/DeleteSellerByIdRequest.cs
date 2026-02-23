using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.DeleteSellerById;

public readonly struct DeleteSellerByIdRequest(Guid id) : IRequest<Result<Unit>>
{
    public Guid Id { get; } = id;
}
