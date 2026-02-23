using MediatR;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.PropertyExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.DeleteProperty;

public class DeletePropertyHandler(ApplicationDbContext dbContext) : IRequestHandler<DeletePropertyRequest, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<Unit>> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _dbContext.DeletePropertyByIdAsync(request.Id, cancellationToken);
        if (!deleted)
            return Result<Unit>.Fail(ErrorCode.NotFound, "Property not found.");

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Ok(Unit.Value);
    }
}
