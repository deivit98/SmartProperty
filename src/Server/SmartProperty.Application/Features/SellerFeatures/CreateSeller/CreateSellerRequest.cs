using MediatR;
using SmartProperty.Domain.Entities.Enums;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.CreateSeller;

/// <summary>
/// Request body for creating a seller. Bind directly from POST body.
/// </summary>
public record CreateSellerRequest(
    string Name,
    SellerType Type,
    string? Email = null,
    string? PhoneNumber = null
) : IRequest<Result<CreateSellerResponse>>;
