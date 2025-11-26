using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class AssignedExperimentGroupsConfiguration() : BaseConfiguration<AssignedExperimentTemplateGroup>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<AssignedExperimentTemplateGroup> builder)
    {
        base.Configure(builder);
        builder.HasIndex(a => new { a.ExperimentId, a.AssignedExperimentProductDeveloperAadGroupId }).IsUnique();

        builder.HasOne(e => e.Experiment)
            .WithMany(a => a.AssignedExperimentTemplateGroups)
            .HasForeignKey(e => e.ExperimentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ProductDeveloperAadGroup)
            .WithMany(a => a.AssignedExperimentTemplateGroups)
            .HasForeignKey(e => e.AssignedExperimentProductDeveloperAadGroupId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
