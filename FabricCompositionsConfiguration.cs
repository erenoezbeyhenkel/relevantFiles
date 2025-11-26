using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class FabricCompositionsConfiguration() : BaseConfiguration<FabricComposition>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<FabricComposition> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
             .IsRequired()
             .HasMaxLength(225)
             .HasComment("Describes the name of the fabric composistion.");

        builder.HasMany(fc => fc.SubstrateFabricCompositions)
            .WithOne(e => e.FabricComposition)
            .HasForeignKey(e => e.FabricCompositionId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(new FabricComposition { Id = 1, Name = "CO", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricComposition { Id = 2, Name = "CV", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricComposition { Id = 3, Name = "PES", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricComposition { Id = 4, Name = "PA", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricComposition { Id = 5, Name = "PAN", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        new FabricComposition { Id = 6, Name = "EL", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });

        //new FabricComposition { Id = 7, Name = "CLY", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) }
        //will be added later
    }
}
