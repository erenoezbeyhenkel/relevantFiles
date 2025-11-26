using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class ProtocolsConfiguration() : BaseConfiguration<Protocol>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Protocol> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the protocol of the SubstrateFamily.");

        builder.Property(p => p.IsActive)
               .IsRequired()
               .HasComment("Describes the status of the current protocol.");


        builder.HasData(new Protocol { Id = 1, Name = "ASTM", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new Protocol { Id = 2, Name = "StiWa", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new Protocol { Id = 3, Name = "AISE 14 v7", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new Protocol { Id = 4, Name = "AU15", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new Protocol { Id = 5, Name = "AISE 26 v7", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new Protocol { Id = 6, Name = "AISE 14 Dye Set", IsActive = true, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) }

                        );
    }
}

