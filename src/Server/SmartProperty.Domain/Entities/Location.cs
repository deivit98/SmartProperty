namespace SmartProperty.Domain.Entities
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string? ZipCode { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Neighborhood { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
