using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class MonitorSubstratesConfiguration() : BaseConfiguration<MonitorSubstrate>(primaryIdNeeded: false)
{
    public override void Configure(EntityTypeBuilder<MonitorSubstrate> builder)
    {
        base.Configure(builder);

        builder.HasKey(a => new { a.MonitorId, a.SubstrateId, a.Position });

        builder.HasIndex(x => x.MonitorId);
        builder.HasIndex(x => x.SubstrateId);
        builder.HasIndex(x => x.Position);


        builder.HasOne(e => e.Monitor)
        .WithMany(a => a.MonitorSubstrates)
        .HasForeignKey(e => e.MonitorId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Substrate)
            .WithMany(a => a.MonitorSubstrates)
            .HasForeignKey(e => e.SubstrateId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}