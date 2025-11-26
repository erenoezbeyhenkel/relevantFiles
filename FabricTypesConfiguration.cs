using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public class FabricTypesConfiguration() : BaseConfiguration<FabricType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<FabricType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the fabric type of the substrate, e.g. cotton,...");

        builder.HasMany(et => et.Substrates)
                .WithOne(e => e.FabricType)
                .HasForeignKey(e => e.FabricTypeId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new FabricType { Id = 1, Name = "Natural", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricType { Id = 2, Name = "Synthetic", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricType { Id = 3, Name = "Mix", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}