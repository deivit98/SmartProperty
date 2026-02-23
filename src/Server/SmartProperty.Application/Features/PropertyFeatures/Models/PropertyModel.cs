using SmartProperty.Application.Features.PropertyFeatures.CreateProperty;
using SmartProperty.Domain.Entities;
using SmartProperty.Domain.Entities.Enums;

namespace SmartProperty.Application.Features.PropertyFeatures.Models;

public class PropertyModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public PropertyType Type { get; init; }
    public PropertyStatus Status { get; init; }
    public int? Bedrooms { get; init; }
    public int? Bathrooms { get; init; }
    public double? Area { get; init; }
    public int? Floor { get; init; }
    public int? YearBuilt { get; init; }
    public int ParkingSpaces { get; init; }
    public bool HasGarden { get; init; }
    public bool HasBalcony { get; init; }
    public decimal? Price { get; init; }
    public string Currency { get; init; } = "EUR";
    public decimal? RentPrice { get; init; }
    public decimal? Deposit { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public Guid? LocationId { get; init; }
    public Guid? SellerId { get; init; }
}

public static class PropertyModelExtensions
{
    public static PropertyModel ToModel(this Property property)
    {
        return new PropertyModel
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Type = property.Type,
            Status = property.Status,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Area = property.Area,
            Floor = property.Floor,
            YearBuilt = property.YearBuilt,
            ParkingSpaces = property.ParkingSpaces,
            HasGarden = property.HasGarden,
            HasBalcony = property.HasBalcony,
            Price = property.Price,
            Currency = property.Currency,
            RentPrice = property.RentPrice,
            Deposit = property.Deposit,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt,
            LocationId = property.LocationId,
            SellerId = property.SellerId
        };
    }

    public static Property ToEntity(this CreatePropertyRequest request, Guid id)
    {
        var now = DateTime.UtcNow;
        return new Property
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            Type = request.Type,
            Status = request.Status,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            Area = request.Area,
            Floor = request.Floor,
            YearBuilt = request.YearBuilt,
            ParkingSpaces = request.ParkingSpaces,
            HasGarden = request.HasGarden,
            HasBalcony = request.HasBalcony,
            Price = request.Price,
            Currency = request.Currency ?? "EUR",
            RentPrice = request.RentPrice,
            Deposit = request.Deposit,
            CreatedAt = now,
            UpdatedAt = now,
            LocationId = request.LocationId,
            SellerId = request.SellerId
        };
    }
}
