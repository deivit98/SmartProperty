using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.EntityConfigurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Currency)
            .HasMaxLength(10);

        builder.HasOne(e => e.Location)
            .WithMany(l => l.Properties)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Seller)
            .WithMany(s => s.Properties)
            .HasForeignKey(e => e.SellerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
