using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;
//TODO: discuss index
public sealed class ProductSetupsConfiguration() : BaseConfiguration<ProductSetup>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ProductSetup> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ExperimentId)
                .IsRequired();

        builder.HasOne(e => e.Experiment)
               .WithMany(e => e.ProductSetups)
               .HasForeignKey(e => e.ExperimentId)
               .OnDelete(DeleteBehavior.Cascade);


        builder.Property(p => p.ProductId)
           .IsRequired();

        builder.HasOne(e => e.Product)
               .WithMany(e => e.ProductSetups)
               .HasForeignKey(e => e.ProductId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Property(p => p.Position)
          .IsRequired()
          .HasComment("Describes the position of the product in the experiment .");

        builder.Property(e => e.DosageMl)
               .HasPrecision(8, 4)
               .HasComment("Dosage of the product in ml.");

        builder.Property(e => e.DosageGram)
               .HasPrecision(8, 4)
               .HasComment("Dosage of the product in gram.");

        builder.Property(e => e.Density)
               .HasPrecision(8, 4)
               .HasComment("Density of the product.");

        builder.Property(e => e.WaterHardness)
               .IsRequired()
               .HasPrecision(5, 2)
               .HasDefaultValue(default)
               .HasComment("Water hardness in '°dH' for the product setup.");

        builder.Property(e => e.WaterLevel)
                  .IsRequired()
                  .HasPrecision(5, 2)
                  .HasDefaultValue(default)
                  .HasComment("Water level in 'L' for the product setup.");

        builder.Property(e => e.ChlorLevel)
                .IsRequired()
                .HasPrecision(5, 2)
                .HasDefaultValue(default)
                .HasComment("Chlor level in 'ppm' of the water for the product setup.");

        builder.Property(p => p.MixingRatio)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("5:1")
                .HasComment("Mixing ratio (Ca2+:Mg2+) of the water for the product setup.");


        builder.Property(p => p.IsDiscardedForAi)
             .IsRequired()
             .HasDefaultValue(false)
             .HasComment("This is used to exclude product related measurement results for the AI model training");


        builder.Property(p => p.IsDiscardedForReporting)
             .IsRequired()
             .HasDefaultValue(false)
             .HasComment("This is used to exclude product related measurement results for the statistics & Power BI reporting");

        builder.Property(p => p.Pieces)
            .IsRequired()
            .HasDefaultValue(0)
            .HasComment("Defines the pieces for Predose products");

        builder.Property(ts => ts.RotationTime)
            .HasComment("Rotation time of the machine in [sec].");

        builder.Property(ts => ts.Drumspeed)
           .HasComment("Drumspeed of the machine in [rpm].");

        builder.Property(ts => ts.ProcessInstructions)
            .HasComment("Free text of process instructions.");



    }
}
