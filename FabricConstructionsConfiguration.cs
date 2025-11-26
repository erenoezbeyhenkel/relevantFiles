using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class FabricConstructionsConfiguration() : BaseConfiguration<FabricConstruction>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<FabricConstruction> builder)
    {
        base.Configure(builder);


        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("Describes the fabric construction for a subtrate.");

        builder.HasMany(fc => fc.Substrates)
               .WithOne(p => p.FabricConstruction)
               .HasForeignKey(p => p.FabricConstructionId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new FabricConstruction { Id = 1, Name = "woven", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricConstruction { Id = 2, Name = "knitted", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}
