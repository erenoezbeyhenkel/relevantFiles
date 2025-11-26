using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class SubstrateProtocolsConfiguration() : BaseConfiguration<SubstrateProtocol>(primaryIdNeeded: false)
{
    public override void Configure(EntityTypeBuilder<SubstrateProtocol> builder)
    {
        base.Configure(builder);
        builder.HasKey(a => new { a.SubstrateId, a.ProtocolId });

        builder.HasIndex(x => x.SubstrateId);
        builder.HasIndex(x => x.ProtocolId);

        builder.HasOne(e => e.Substrate)
            .WithMany(a => a.SubstrateProtocols)
            .HasForeignKey(e => e.SubstrateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Protocol)
            .WithMany(a => a.SubstrateProtocols)
            .HasForeignKey(e => e.ProtocolId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
