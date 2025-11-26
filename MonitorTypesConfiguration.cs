using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public class MonitorTypesConfiguration() : BaseConfiguration<MonitorType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<MonitorType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("Describes the type of the monitor, e.g. SOAR, Stain, WhiteFabric, WFK, CFT...");

        builder.HasMany(et => et.Monitors)
                .WithOne(e => e.MonitorType)
                .HasForeignKey(e => e.MonitorTypeId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new MonitorType { Id = 1, Name = "SOAR", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        //new MonitorType { Id = 2, Name = "WFK", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 2, Name = "Stain (single)", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 3, Name = "Stain (multiple)", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 4, Name = "Pilling", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 5, Name = "White Fabric", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 6, Name = "Color Care", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new MonitorType { Id = 7, Name = "Virtual", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}