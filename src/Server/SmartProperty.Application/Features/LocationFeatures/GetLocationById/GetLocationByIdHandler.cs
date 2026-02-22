using MediatR;
using SmartProperty.Application.Features.Models;
using SmartProperty.Infrastructure.Data;
using SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Read;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.LocationFeatures.GetLocationById;

public class GetLocationByIdHandler(ApplicationDbContext dbContext) : IRequestHandler<GetLocationByIdRequest, Result<GetLocationByIdResponse>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetLocationByIdResponse>> Handle(GetLocationByIdRequest request, CancellationToken cancellationToken)
    {
        var location = await _dbContext.GetLocationByID(request.Id, cancellationToken);

        if (location is null)
            return Result<GetLocationByIdResponse>.Fail(ErrorCode.NotFound, "Location not found.");

        var response = new GetLocationByIdResponse(location.ToModel());
        return Result<GetLocationByIdResponse>.Ok(response);
    }
}
