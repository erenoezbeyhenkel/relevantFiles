using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class ClusterTypesConfiguration() : BaseConfiguration<ClusterType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ClusterType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("Describes the cluster type for a stain. In other words, which ingredient (Enzym, Pigment, Bleach, Surfactant) has the greatest effect on a particular stain.");

        builder.HasMany(pt => pt.SubstrateFamilyClusterTypes)
               .WithOne(p => p.ClusterType)
               .HasForeignKey(p => p.ClusterTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new ClusterType { Id = 1, SubstrateCategoryId = 1, Name = "Bleach", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ClusterType { Id = 2, SubstrateCategoryId = 1, Name = "Enzyme", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ClusterType { Id = 3, SubstrateCategoryId = 1, Name = "Surfactant", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ClusterType { Id = 4, SubstrateCategoryId = 1, Name = "Mechanic", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}

