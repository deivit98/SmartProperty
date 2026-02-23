using MediatR;
using SmartProperty.Domain.Entities.Enums;
using SmartProperty.Result;

namespace SmartProperty.Application.Features.PropertyFeatures.CreateProperty;

/// <summary>
/// Request body for creating a property. Bind directly from POST body.
/// </summary>
public record CreatePropertyRequest(
    string Title,
    PropertyType Type,
    PropertyStatus Status,
    string? Description = null,
    int? Bedrooms = null,
    int? Bathrooms = null,
    double? Area = null,
    int? Floor = null,
    int? YearBuilt = null,
    int ParkingSpaces = 0,
    bool HasGarden = false,
    bool HasBalcony = false,
    decimal? Price = null,
    string? Currency = null,
    decimal? RentPrice = null,
    decimal? Deposit = null,
    Guid? LocationId = null,
    Guid? SellerId = null
) : IRequest<Result<CreatePropertyResponse>>;
