using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.LocationExtensions.Write
{
    public static class SaveLocation
    {
        public static async Task<Location?> GetTrackedLocationByID(this ApplicationDbContext dbContext, Guid locationID, CancellationToken ct)
        {
            return
                await dbContext
                .Locations
                .FirstOrDefaultAsync(l => l.Id == locationID, ct);
        }

        public static async Task<Location> AddLocationAsync(this ApplicationDbContext db, Location location, CancellationToken ct = default)
        {
            await db.Locations.AddAsync(location, ct);
            return location;
        }

        public static async Task<bool> DeleteLocationByIdAsync(this ApplicationDbContext db,Guid locationId,CancellationToken ct = default)
        {
            var location = await db.Locations.FirstOrDefaultAsync(l => l.Id == locationId, ct);

            if (location is null)
                return false;

            db.Locations.Remove(location);
            return true;
        }
    }
}
