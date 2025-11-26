using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public class MonitorSetupsConfiguration() : BaseConfiguration<MonitorSetup>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<MonitorSetup> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ExperimentId)
                .IsRequired();

        builder.HasOne(e => e.Experiment)
               .WithMany(e => e.MonitorSetups)
               .HasForeignKey(e => e.ExperimentId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.NumberOfSamples)
               .IsRequired()
               .HasDefaultValue(1)
               .HasComment("Describes the number physical monitors in one repetition which is to put into a washing machine.");

        builder.Property(p => p.IsPrewashed)
               .IsRequired()
               .HasDefaultValue(false)
               .HasComment("Describes the pretreatment of the monitor. " +
               "Prewashed means that the monitor were prewashed 3 times with the respective product.");

        builder.Property(p => p.IsUvMeasurement)
             .IsRequired()
             .HasDefaultValue(false)
             .HasComment("Describes a measurement device setup for UV based" +
             " measurements. This is only valid for Datacolor measurement devices.");

        builder.Property(p => p.IsSoarRequested)
               .IsRequired()
               .HasDefaultValue(false)
               .HasComment("Describes the status if the monitor type is requested by an order in the SOAR system.");

        builder.Property(p => p.SoarRequestedDate)
           .HasComment("Describes the timepoint/date where wash.net operator requests the monitor type in the SOAR system. " +
           "If 'IsSoarRequested' == true Then 'SoarRequestedDate' has a date Else 'SoarRequestedDate' is null ");


        builder.Property(p => p.IsDmcAssigned)
           .IsRequired()
           .HasDefaultValue(false)
           .HasComment("Describes the status if the monitor assignment is done by an operator with the DMC Reader.");

        builder.Property(p => p.DmcAssignedDate)
          .HasComment("Describes the timepoint/date where wash.net operator assigned the dmc for all frame monitors.");

        builder.Property(p => p.IsMeasured)
           .IsRequired()
           .HasDefaultValue(false)
           .HasComment("Describes the status if all monitors for all washCycles are completly measured.");

        builder.Property(p => p.MeasuredDate)
          .HasComment("Describes the timepoint/date where wash.net operator is finished to measure all wash cycles. ");


        //enforces uniqueness across the named fields.
        builder.HasIndex(ms => new
        {
            ms.ExperimentId,
            ms.DeviceTypeId,
            ms.MonitorId,
            ms.IsPrewashed,
            ms.IsUvMeasurement
        })
        .IsUnique();



    }
}
