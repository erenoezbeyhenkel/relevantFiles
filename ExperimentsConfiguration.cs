using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public class ExperimentsConfiguration() : BaseConfiguration<Experiment>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Experiment> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.ExperimentStatusId).IsRequired();

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasComment("Defines if the template is active or not.");

        builder.Property(e => e.IsExternal)
          .IsRequired()
          .HasDefaultValue(false)
          .HasComment("Defines if the experiment external.");

        builder.Property(e => e.StatusOpenDate)
            .IsRequired()
            .HasComment("Defines the date when the experiment status changed to OPEN");

        builder.Property(e => e.StatusValidationDate)
            .HasComment("Defines the date when the experiment status changed to INVALIDATION");

        builder.Property(e => e.StatusPreparationDate)
        .HasComment("Defines the date when the experiment status changed to INPREPARATION");

        builder.Property(e => e.StatusMeasurementDate)
            .HasComment("Defines the date when the experiment status changed to INMEASUREMENT");

        builder.Property(e => e.StatusCompletedDate)
        .HasComment("Defines the date when the experiment status changed to COMPLETE");

        builder.Property(e => e.ProductDeliveryDate)
            .HasComment("Defines the delivery date of products");

        builder.Property(e => e.StartingCalendarWeekDate)
            .HasComment("Defines the calendar week day of starting to perform experiment");

        builder.HasOne(e => e.Order)
               .WithMany(e => e.Experiments)
               .HasForeignKey(e => e.OrderId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(p => p.Description)
                .HasComment("Short description of the experiment.");

        builder.Property(p => p.Comment)
               .HasComment("Additional comments for the experiment. Used for excel export");

        builder.Property(p => p.NumberOfRepetition)
               .IsRequired()
               .HasComment("Repetition number of the experiment.");

        builder.Property(e => e.TemplateExperimentId)
            .HasComment("Experiment id of the template experiment which was used to create this experiment.");

        builder.Property(e => e.RequestedProgram)
            .HasMaxLength(200)
            .HasComment("Defines the washing machine program which is requested.");

        builder.Property(e => e.RequestedTemperature)
            .HasDefaultValue(0)
            .HasComment("Defines the washing machine temperature which is requested.");




    }
}
