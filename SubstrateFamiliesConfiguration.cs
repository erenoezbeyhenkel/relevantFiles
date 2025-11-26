using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class SubstrateFamiliesConfiguration() : BaseConfiguration<SubstrateFamily>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<SubstrateFamily> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the substrate family as an overall category of a substrate like 'lipstick'.");

        builder.Property(m => m.Comment)
            .HasComment("Additional comments for substrate family.");


        builder.HasMany(et => et.Substrates)
        .WithOne(e => e.SubstrateFamily)
        .HasForeignKey(e => e.SubstrateFamilyId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
