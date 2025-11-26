using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class ProductionModesConfiguration() : BaseConfiguration<ProductionMode>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ProductionMode> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the fabric type of the substrate, e.g. cotton,...");

        builder.HasMany(et => et.Substrates)
                .WithOne(e => e.ProductionMode)
                .HasForeignKey(e => e.ProductionModeId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new ProductionMode { Id = 1, Name = "Hand made", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                       new ProductionMode { Id = 2, Name = "Machine made", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}