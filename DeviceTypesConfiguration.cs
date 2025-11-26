using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class DeviceTypesConfiguration() : BaseConfiguration<DeviceType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<DeviceType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("Describes the product type of the product. i.e: Liquid, Liquid concentrates, Solid, Predose Liquids...");

        builder.HasMany(pt => pt.MonitorSetups)
         .WithOne(p => p.DeviceType)
         .HasForeignKey(p => p.DeviceTypeId)
         .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(pt => pt.MeasurementDevices)
              .WithOne(p => p.DeviceType)
              .HasForeignKey(p => p.DeviceTypeId)
              .OnDelete(DeleteBehavior.NoAction);



        builder.HasData(new DeviceType { Id = 1, Name = "Mach5", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new DeviceType { Id = 2, Name = "DataColor", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new DeviceType { Id = 4, Name = "Videometer", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new DeviceType { Id = 5, Name = "US Robot", CreatedById = "SystemUser", CreatedAt = new DateTime(2025, 5, 26), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2025, 5, 26) },
                        new DeviceType { Id = 6, Name = "External Mach5 Upload", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new DeviceType { Id = 7, Name = "External Excel Upload", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}

