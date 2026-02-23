using MediatR;
using SmartProperty.Application.Features.SellerFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.GetSellerByEmail;

public class GetSellerByEmailHandler(ApplicationDbContext dbContext) : IRequestHandler<GetSellerByEmailRequest, Result<GetSellerByEmailResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetSellerByEmailResponse>> Handle(GetSellerByEmailRequest request, CancellationToken cancellationToken)
    {
        var seller = await _dbContext.GetSellerByEmailAsync(request.Email, cancellationToken);

        if (seller is null)
            return Result<GetSellerByEmailResponse>.Fail(ErrorCode.NotFound, "Seller not found.");

        var response = new GetSellerByEmailResponse(seller.ToModel());
        return Result<GetSellerByEmailResponse>.Ok(response);
    }
}
