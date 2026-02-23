using MediatR;
using SmartProperty.Application.Features.PropertyFeatures.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.PropertyExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.GetPropertyById;

public class GetPropertyByIdHandler(ApplicationDbContext dbContext) : IRequestHandler<GetPropertyByIdRequest, Result<GetPropertyByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetPropertyByIdResponse>> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
    {
        var property = await _dbContext.GetPropertyByIdAsync(request.Id, cancellationToken);

        if (property is null)
            return Result<GetPropertyByIdResponse>.Fail(ErrorCode.NotFound, "Property not found.");

        var response = new GetPropertyByIdResponse(property.ToModel());
        return Result<GetPropertyByIdResponse>.Ok(response);
    }
}
