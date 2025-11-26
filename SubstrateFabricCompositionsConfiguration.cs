using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class SubstrateFabricCompositionsConfiguration() : BaseConfiguration<SubstrateFabricComposition>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<SubstrateFabricComposition> builder)
    {
        base.Configure(builder);

        builder.Property(sfc => sfc.SubstrateId)
            .IsRequired()
            .HasComment("Represents the substrate id.");

        builder.Property(sfc => sfc.FabricCompositionId)
            .IsRequired()
            .HasComment("Represents the fabric composition id.");

        builder.Property(x => x.Percentage)
            .IsRequired()
            .HasPrecision(5, 2)
            .HasDefaultValue(default);

        builder.HasOne(x => x.Substrate)
            .WithMany(x => x.SubstrateFabricCompositions)
            .HasForeignKey(x => x.SubstrateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
