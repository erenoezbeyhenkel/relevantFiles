using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class TemplateSettingsConfiguration() : BaseConfiguration<TemplateSetting>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<TemplateSetting> builder)
    {
        base.Configure(builder);

        builder.Property(ts => ts.ExperimentTemplateId).IsRequired();

        builder.HasOne(ts => ts.ExperimentTemplate)
            .WithOne(e => e.TemplateSetting)
            .HasForeignKey<TemplateSetting>(te => te.ExperimentTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ts => ts.WaterHardness)
                .IsRequired()
                .HasPrecision(5, 2)
                .HasDefaultValue(default)
                .HasComment("Water hardness in '°dH' for the experiment template.");

        builder.Property(ts => ts.WaterLevel)
                  .IsRequired()
                  .HasPrecision(5, 2)
                  .HasDefaultValue(default)
                  .HasComment("Water level in 'L' for the experiment template.");

        builder.Property(ts => ts.ChlorLevel)
                .IsRequired()
                .HasPrecision(5, 2)
                .HasDefaultValue(default)
                .HasComment("Chlor level in 'ppm' of the water for the experiment template.");

        builder.Property(ts => ts.MixingRatio)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("5:1")
                .HasComment("Mixing ratio (Ca2+:Mg2+) of the water for the experiment template.");

        builder.Property(ts => ts.RotationTime)
            .HasComment("Rotation time of the machine in [sec].");

        builder.Property(ts => ts.Drumspeed)
           .HasComment("Drumspeed of the machine in [rpm].");

    }
}
