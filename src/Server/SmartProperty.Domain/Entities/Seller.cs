using SmartProperty.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartProperty.Domain.Entities
{
    public class Seller
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public SellerType Type { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        // One owner can have many properties
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
