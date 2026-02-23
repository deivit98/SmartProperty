using MediatR;
using SmartProperty.Application.Features.SellerFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.CreateSeller;

public class CreateSellerHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateSellerRequest, Result<CreateSellerResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<CreateSellerResponse>> Handle(CreateSellerRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return Result<CreateSellerResponse>.Fail(ErrorCode.EmptyParameter, "Name is required.");

        var seller = request.ToEntity(Guid.NewGuid());
        await _dbContext.AddSellerAsync(seller, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result<CreateSellerResponse>.Ok(new CreateSellerResponse(seller.ToModel()));
    }
}
