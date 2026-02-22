namespace SmartProperty.Application.Features.GetLocationById;

public readonly struct GetLocationByIdResponse
{
    public Guid Id { get; }
    public string Address { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string? ZipCode { get; }
    public double? Latitude { get; }
    public double? Longitude { get; }
    public string? Neighborhood { get; }

    public GetLocationByIdResponse(Guid id, string address, string city, string state, string country, string? zipCode, double? latitude, double? longitude, string? neighborhood)
    {
        Id = id;
        Address = address;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Latitude = latitude;
        Longitude = longitude;
        Neighborhood = neighborhood;
    }
}
