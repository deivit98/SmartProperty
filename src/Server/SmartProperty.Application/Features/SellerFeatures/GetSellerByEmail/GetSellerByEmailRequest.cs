using MediatR;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.SellerFeatures.GetSellerByEmail;

public record GetSellerByEmailRequest(string Email) : IRequest<Result<GetSellerByEmailResponse>>;
