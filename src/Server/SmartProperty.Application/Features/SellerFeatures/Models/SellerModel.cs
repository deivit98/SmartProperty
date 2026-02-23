using SmartProperty.Application.Features.SellerFeatures.CreateSeller;
using SmartProperty.Application.Features.SellerFeatures.UpdateSellerContact;
using SmartProperty.Domain.Entities;
using SmartProperty.Domain.Entities.Enums;

namespace SmartProperty.Application.Features.SellerFeatures.Models
{
    public class SellerModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public SellerType Type { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
    }

    public static class SellerModelExtensions
    {
        public static SellerModel ToModel(this Seller seller)
        {
            return new SellerModel
            {
                Id = seller.Id,
                Name = seller.Name,
                Type = seller.Type,
                Email = seller.Email,
                PhoneNumber = seller.PhoneNumber
            };
        }

        public static Seller ToEntity(this CreateSellerRequest request, Guid id)
        {
            return new Seller
            {
                Id = id,
                Name = request.Name,
                Type = request.Type,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
        }

        public static void ApplyContactTo(this UpdateSellerContactBody body, Seller seller)
        {
            seller.Email = body.Email;
            seller.PhoneNumber = body.PhoneNumber;
        }
    }
}
