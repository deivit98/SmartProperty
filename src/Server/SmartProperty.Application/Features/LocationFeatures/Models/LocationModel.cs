using SmartProperty.Application.Features.LocationFeatures.CreateLocation;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Application.Features.LocationFeatures.Models
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

    public static class LocationModelExtensions
    {
        public static LocationModel ToModel(this Location location)
        {
            return new LocationModel
            {
                Id = location.Id,
                Address = location.Address,
                City = location.City,
                State = location.State,
                Country = location.Country,
                ZipCode = location.ZipCode,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Neighborhood = location.Neighborhood
            };
        }

        public static LocationModel ToLocationModel(this CreateLocationRequest request)
        {
            return new LocationModel
            {
                Id = Guid.Empty,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Neighborhood = request.Neighborhood
            };
        }

        public static Location ToEntity(this LocationModel request, Guid id)
        {
            return new Location
            {
                Id = id,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Neighborhood = request.Neighborhood
            };
        }

        public static void ApplyTo(this LocationModel request, Location location)
        {
            location.Address = request.Address;
            location.City = request.City;
            location.State = request.State;
            location.Country = request.Country;
            location.ZipCode = request.ZipCode;
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            location.Neighborhood = request.Neighborhood;
        }
    }
}
