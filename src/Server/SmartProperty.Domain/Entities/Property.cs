using SmartProperty.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartProperty.Domain.Entities
{
    public class Property
    {
        [Key]
        public Guid Id { get; set; }

        // Basic Info
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }

        // Physical Characteristics
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public double? Area { get; set; }
        public int? Floor { get; set; }
        public int? YearBuilt { get; set; }
        public int ParkingSpaces { get; set; }
        public bool HasGarden { get; set; }
        public bool HasBalcony { get; set; }

        // Pricing
        public decimal? Price { get; set; }
        public string Currency { get; set; } = "EUR";
        public decimal? RentPrice { get; set; }
        public decimal? Deposit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public Guid? LocationId { get; set; }
        public Location? Location { get; set; }

        public Guid? SellerId { get; set; }
        public Seller? Seller { get; set; }
    }
}
