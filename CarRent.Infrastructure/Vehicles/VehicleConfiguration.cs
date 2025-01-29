using CarRent.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRent.Infrastructure.Vehicles;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(v=>v.Reservations)
            .WithOne(r => r.Vehicle)
            .HasForeignKey(r => r.VehicleId);
    }
}