using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public class ExperimentTypesConfiguration() : BaseConfiguration<ExperimentType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ExperimentType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasComment("Describes the experiment type of the experiment. i.e: Stain Removal");

        builder.Property(p => p.WorkInstructionPath)
            .HasMaxLength(500)
            .HasComment("Path to the work instruction documentation related to the experiment type. ");

        builder.HasMany(et => et.Experiments)
               .WithOne(e => e.ExperimentType)
               .HasForeignKey(e => e.ExperimentTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new ExperimentType { Id = 1, Name = "Stain Removal", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 2, Name = "Add on Stain Removal: White Fabrics", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 3, Name = "Graying Inhibition - Stress Test", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 4, Name = "Secondary Whiteness Test", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 5, Name = "Dye Transfer Inhibition - Linitest", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 6, Name = "Longterm/Multi Cycle Testing", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 7, Name = "Wash & Wear Test", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 8, Name = "Pilling Test", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 9, Name = "Sniff Test", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 10, Name = "EPS biofilm", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new ExperimentType { Id = 11, Name = "Color Care", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}
