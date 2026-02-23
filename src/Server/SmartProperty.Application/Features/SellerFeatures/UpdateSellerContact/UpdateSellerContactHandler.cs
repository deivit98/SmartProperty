using MediatR;
using SmartProperty.Application.Features.SellerFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.UpdateSellerContact;

public class UpdateSellerContactHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateSellerContactRequest, Result<UpdateSellerContactResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<UpdateSellerContactResponse>> Handle(UpdateSellerContactRequest request, CancellationToken cancellationToken)
    {
        var seller = await _dbContext.GetTrackedSellerByIdAsync(request.Id, cancellationToken);
        if (seller is null)
            return Result<UpdateSellerContactResponse>.Fail(ErrorCode.NotFound, "Seller not found.");

        request.Body.ApplyContactTo(seller);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<UpdateSellerContactResponse>.Ok(new UpdateSellerContactResponse(seller.ToModel()));
    }
}
