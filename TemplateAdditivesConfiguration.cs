using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class TemplateAdditivesConfiguration() : BaseConfiguration<TemplateAdditive>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<TemplateAdditive> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.ExperimentId)
            .IsRequired();

        builder.Property(a => a.AdditiveTypeId)
            .IsRequired();

        builder.HasOne(e => e.Experiment)
               .WithMany(e => e.TemplateAdditives)
               .HasForeignKey(e => e.ExperimentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasComment("Describes the amount of the additive type.");

        builder.Property(p => p.IsProductDependent)
               .IsRequired()
               .HasDefaultValue(false)
               .HasComment("Describes if the selected additive type is product dependend.");

        builder.HasIndex(a => new
        {
            a.ExperimentId,
            a.AdditiveTypeId
        })
        .IsUnique();
    }
}
