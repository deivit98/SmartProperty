namespace SmartProperty.Application.Features.Models
{
    public class LocationModel
    {
        public Guid Id { get; init; }
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string State { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;
        public string? ZipCode { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
        public string? Neighborhood { get; init; }
    }
}
