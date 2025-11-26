using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class WashCycleSetups() : BaseConfiguration<WashCycleSetup>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<WashCycleSetup> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.MonitorSetupId)
                .IsRequired();

        builder.HasOne(e => e.MonitorSetup)
               .WithMany(e => e.WashCycleSetups)
               .HasForeignKey(e => e.MonitorSetupId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.MonitorBatch)
            .WithMany(e => e.WashCycleSetups)
            .HasForeignKey(e => e.MonitorBatchId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(wcs => wcs.IsInitial)
                .IsRequired()
                .HasComment("Wash cycle is initial means, measured values are initial/reference values.");

        builder.Property(wcs => wcs.WashCycle)
                .IsRequired().HasComment("Describes the wash cycle number.");

        builder.Property(wcs => wcs.MeasurementPointsOnSubstrate)
                .HasComment("Describes the number measurements on a substrate.");

        builder.Property(wcs => wcs.IsMeasurementComplete)
        .HasComment("Describes the measurement completion status.");

        builder.Property(wcs => wcs.CompleteDate)
        .HasComment("Describes the measurement complete date.");

        //enforces uniqueness across the named fields.
        builder.HasIndex(wcs => new
        {
            wcs.MonitorSetupId,
            wcs.IsInitial,
            wcs.WashCycle
        })
        .IsUnique();
    }
}
