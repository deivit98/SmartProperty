using Microsoft.EntityFrameworkCore;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.Data.ExtensionMethods.SellerExtensions.Write
{
    public static class SaveSeller
    {
        public static async Task<Seller> AddSellerAsync(this ApplicationDbContext db,Seller seller,CancellationToken ct = default)
        {
            await db.Sellers.AddAsync(seller, ct);
            return seller; // tracked
        }

        public static async Task<Seller?> GetTrackedSellerByIdAsync(this ApplicationDbContext db,Guid sellerId,CancellationToken ct = default)
        {
            return await db.Sellers
                           .FirstOrDefaultAsync(s => s.Id == sellerId, ct);
        }

        public static async Task<bool> DeleteSellerByIdAsync(this ApplicationDbContext db,Guid sellerId,CancellationToken ct = default)
        {
            var seller = await db.Sellers
                                 .FirstOrDefaultAsync(s => s.Id == sellerId, ct);

            if (seller is null)
                return false;

            db.Sellers.Remove(seller);
            return true;
        }
    }
}
