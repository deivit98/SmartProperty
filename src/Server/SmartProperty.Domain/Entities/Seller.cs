using SmartProperty.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace SmartProperty.Domain.Entities
{
    public class Seller : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SellerType Type { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
