using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public class ExperimentStatusesConfiguration() : BaseConfiguration<ExperimentStatus>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ExperimentStatus> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("Name of the status for an experiment.");

        builder.HasMany(es => es.Experiments)
              .WithOne(e => e.ExperimentStatus)
              .HasForeignKey(e => e.ExperimentStatusId)
              .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new ExperimentStatus { Id = 1, Name = "Open", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentStatus { Id = 4, Name = "Validation", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentStatus { Id = 8, Name = "Preparation", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentStatus { Id = 16, Name = "Measurement", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentStatus { Id = 32, Name = "Completed", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });

    }
}
