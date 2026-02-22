using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;
using SmartProperty.Domain.Entities.Enums;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Read
{
    public static class GetSeller
    {
        public static Task<Seller?> GetSellerByIdAsync(this ApplicationDbContext db, Guid sellerId, CancellationToken ct = default)
        {
            return db.Sellers
                     .AsNoTracking()
                     .FirstOrDefaultAsync(s => s.Id == sellerId, ct);
        }

        public static Task<Seller?> GetSellerByEmailAsync(this ApplicationDbContext db, string email, CancellationToken ct = default)
        {
            return db.Sellers
                     .AsNoTracking()
                     .FirstOrDefaultAsync(s => s.Email == email, ct);
        }

        public static Task<List<Seller>> GetSellersByTypeAsync(this ApplicationDbContext db, SellerType type, CancellationToken ct = default)
        {
            return db.Sellers
                     .AsNoTracking()
                     .Where(s => s.Type == type)
                     .ToListAsync(ct);
        }

        public static Task<Seller?> GetSellerWithPropertiesAsync(this ApplicationDbContext db, Guid sellerId, CancellationToken ct = default)
        {
            return db.Sellers
                     .Include(s => s.Properties)
                     .AsNoTracking()
                     .FirstOrDefaultAsync(s => s.Id == sellerId, ct);
        }
    }
}
