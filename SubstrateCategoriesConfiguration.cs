using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class SubstrateCategoriesConfiguration() : BaseConfiguration<SubstrateCategory>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<SubstrateCategory> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the substrate category of the substrate family, i.e: Stain, Fabric, BackgroundFabric,...");

        builder.HasMany(sc => sc.SubstrateFamilies)
               .WithOne(s => s.SubstrateCategory)
               .HasForeignKey(s => s.SubstrateCategoryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(sc => sc.ClusterTypes)
              .WithOne(s => s.SubstrateCategory)
              .HasForeignKey(s => s.SubstrateCategoryId)
              .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new SubstrateCategory { Id = 1, Name = "Stain", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new SubstrateCategory { Id = 2, Name = "Fabric", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}

