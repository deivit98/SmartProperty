using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;
using SmartProperty.Infrastructure.Data;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.PropertyExtensions.Read;

public static class GetProperty
{
    public static Task<Property?> GetPropertyByIdAsync(this ApplicationDbContext db, Guid propertyId, CancellationToken ct = default)
    {
        return db.Properties
                 .AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == propertyId, ct);
    }
}
