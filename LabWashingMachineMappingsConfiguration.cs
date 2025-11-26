using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class LabWashingMachineMappingsConfiguration() : BaseConfiguration<LabWashingMachineMapping>(primaryIdNeeded: false)
{
    public override void Configure(EntityTypeBuilder<LabWashingMachineMapping> builder)
    {
        base.Configure(builder);

        builder.HasKey(a => new { a.LabWashingMachineId, a.WashingMachineId });

        builder.HasIndex(x => x.LabWashingMachineId);
        builder.HasIndex(x => x.WashingMachineId);


        builder.HasOne(e => e.WashingMachine)
        .WithMany(a => a.LabWashingMachineMappings)
        .HasForeignKey(e => e.WashingMachineId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.LabWashingMachine)
            .WithMany(a => a.LabWashingMachineMappings)
            .HasForeignKey(e => e.LabWashingMachineId)
            .OnDelete(DeleteBehavior.Cascade);

        //builder.HasData(new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 1, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 2, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 3, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 4, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 5, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 6, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 7, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 8, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 9, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 10, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 11, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 123, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 124, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 1, WashingMachineId = 125, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },

        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 1, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 2, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 3, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 4, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 5, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 6, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 7, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 8, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 9, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 10, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 11, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 123, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 124, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 2, WashingMachineId = 125, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },

        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 1, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 2, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 3, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 4, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 5, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 6, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 7, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 8, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 9, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 10, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 11, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 123, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 124, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 3, WashingMachineId = 125, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },

        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 12, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 13, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 14, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 15, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 16, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 17, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 10, WashingMachineId = 119, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },

        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 12, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 13, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 14, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 15, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 16, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 17, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachineMapping { LabWashingMachineId = 11, WashingMachineId = 119, CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });

    }
}