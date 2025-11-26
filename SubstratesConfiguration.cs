using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public class SubstratesConfiguration() : BaseConfiguration<Substrate>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Substrate> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Name of the a particular specification of substate family e.g. L'Oreal Paris");


        builder.Property(s => s.Label)
         .HasMaxLength(255)
         .HasComment("Specific label name e.g. CFT CS-103");


        builder.Property(s => s.ReleaseYear)
                 .IsRequired()
                 .HasComment("Release year of the substrate");

        builder.Property(s => s.HugoReference)
                .HasMaxLength(40)
                .HasComment("Hugo Reference of the consumables.");

        builder.Property(s => s.FabricConstructionId)
            .HasComment("Defines the fabric construction");

        builder.Property(s => s.FabricColor)
            .HasMaxLength(40)
            .HasComment("Defines the color of the fabric.");

        builder.Property(s => s.SoarStainingMaterialDefinitionFileName)
                .HasMaxLength(255)
                .HasComment("Name of the SOAR Staining Material Definition file name");

        builder.Property(m => m.Comment)
            .HasComment("Additional comments for substrate.");


        builder.HasMany(et => et.SubstrateFrames)
            .WithOne(e => e.Substrate)
            .HasForeignKey(e => e.SubstrateId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
