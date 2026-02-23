using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.UpdateSellerContact;

/// <summary>
/// Body for updating seller contact info (email and phone only). Bind from PUT body.
/// </summary>
public record UpdateSellerContactBody(string? Email, string? PhoneNumber);

/// <summary>
/// Request for updating seller contact. Id comes from the route; body is <see cref="UpdateSellerContactBody"/>.
/// </summary>
public record UpdateSellerContactRequest(Guid Id, UpdateSellerContactBody Body) : IRequest<Result<UpdateSellerContactResponse>>;
