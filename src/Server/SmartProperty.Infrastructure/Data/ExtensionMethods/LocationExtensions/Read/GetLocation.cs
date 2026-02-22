using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Read
{
    public static class GetLocation
    {

        public static async Task<Location?> GetLocationByID(this ApplicationDbContext dbContext, Guid locationID, CancellationToken ct = default)
        {
            return 
                await dbContext
                .Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == locationID, ct);
        }

        public static async Task<Location?> GetLocationByPropertyID(this ApplicationDbContext dbContext, Guid propertyID, CancellationToken ct = default)
        {
            return 
                await dbContext
                .Properties
                .AsNoTracking()
                .Where(p => p.Id == propertyID)
                .Select(p => p.Location)
                .FirstOrDefaultAsync(ct);
        }
    }
}
