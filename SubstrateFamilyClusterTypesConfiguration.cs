using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class SubstrateFamilyClusterTypesConfiguration() : BaseConfiguration<SubstrateFamilyClusterType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<SubstrateFamilyClusterType> builder)
    {
        base.Configure(builder);

        builder.Property(sfct => sfct.SubstrateFamilyId)
            .IsRequired()
            .HasComment("Represents the substrate family id.");

        builder.Property(sfct => sfct.ClusterTypeId)
            .IsRequired()
            .HasComment("Represents the cluster type id.");

        builder.HasOne(x => x.SubstrateFamily)
           .WithMany(x => x.SubstrateFamilyClusterTypes)
           .HasForeignKey(x => x.SubstrateFamilyId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
