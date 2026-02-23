using MediatR;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.DeleteSellerById;

public class DeleteSellerByIdHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteSellerByIdRequest, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<Unit>> Handle(DeleteSellerByIdRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _dbContext.DeleteSellerByIdAsync(request.Id, cancellationToken);
        if (!deleted)
            return Result<Unit>.Fail(ErrorCode.NotFound, "Seller not found.");

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Ok(Unit.Value);
    }
}
