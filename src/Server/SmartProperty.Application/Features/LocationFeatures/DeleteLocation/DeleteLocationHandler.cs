using MediatR;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Write;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.DeleteLocation;

public class DeleteLocationHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteLocationRequest, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<Unit>> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
    {
        var deleted = await _dbContext.DeleteLocationByIdAsync(request.Id, cancellationToken);
        if (!deleted)
            return Result<Unit>.Fail(ErrorCode.NotFound, "Location not found.");

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result<Unit>.Ok(Unit.Value);
    }
}
