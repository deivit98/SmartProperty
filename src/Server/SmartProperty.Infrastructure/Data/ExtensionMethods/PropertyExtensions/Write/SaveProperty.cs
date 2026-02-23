using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;
using SmartProperty.Infrastructure.Data;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.PropertyExtensions.Write;

public static class SaveProperty
{
    public static async Task<Property> AddPropertyAsync(this ApplicationDbContext db, Property property, CancellationToken ct = default)
    {
        await db.Properties.AddAsync(property, ct);
        return property;
    }

    public static async Task<bool> DeletePropertyByIdAsync(this ApplicationDbContext db, Guid propertyId, CancellationToken ct = default)
    {
        var property = await db.Properties
                              .FirstOrDefaultAsync(p => p.Id == propertyId, ct);

        if (property is null)
            return false;

        db.Properties.Remove(property);
        return true;
    }
}
