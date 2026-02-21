using SmartProperty.Domain.Entities.Enums;

namespace SmartProperty.Domain.Entities
{
    public class Seller
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public SellerType Type { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
