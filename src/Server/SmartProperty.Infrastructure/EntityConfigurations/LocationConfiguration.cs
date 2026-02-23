using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.EntityConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.State)
            .HasMaxLength(50);

        builder.Property(e => e.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(e => e.Properties)
            .WithOne(p => p.Location)
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
