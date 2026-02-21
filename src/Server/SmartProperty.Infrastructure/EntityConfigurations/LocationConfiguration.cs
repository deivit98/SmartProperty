using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartProperty.Domain.Entities;

namespace SmartProperty.Infrastructure.EntityConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Address)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(e => e.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.State)
            .HasMaxLength(100);

        builder.Property(e => e.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ZipCode)
            .HasMaxLength(20);

        builder.Property(e => e.Neighborhood)
            .HasMaxLength(200);
    }
}
