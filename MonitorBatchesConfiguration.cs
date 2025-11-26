using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class MonitorBatchesConfiguration() : BaseConfiguration<MonitorBatch>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<MonitorBatch> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.MonitorId)
            .IsRequired();

        builder.Property(p => p.BatchId)
           .IsRequired();

        builder.HasOne(e => e.Monitor)
            .WithMany(e => e.MonitorBatches)
            .HasForeignKey(e => e.MonitorId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
