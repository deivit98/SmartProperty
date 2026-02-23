using MediatR;
using SmartProperty.Application.Features.SellerFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.GetSellerById;

public class GetSellerByIdHandler(ApplicationDbContext dbContext) : IRequestHandler<GetSellerByIdRequest, Result<GetSellerByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetSellerByIdResponse>> Handle(GetSellerByIdRequest request, CancellationToken cancellationToken)
    {
        var seller = await _dbContext.GetSellerByIdAsync(request.Id, cancellationToken);

        if (seller is null)
            return Result<GetSellerByIdResponse>.Fail(ErrorCode.NotFound, "Seller not found.");

        var response = new GetSellerByIdResponse(seller.ToModel());
        return Result<GetSellerByIdResponse>.Ok(response);
    }
}
