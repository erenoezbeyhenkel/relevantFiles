using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class ProductSetupAdditivesConfiguration() : BaseConfiguration<ProductSetupAdditive>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ProductSetupAdditive> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.ProductSetupId)
            .IsRequired();

        builder.Property(a => a.AdditiveTypeId)
            .IsRequired();

        builder.HasOne(e => e.ProductSetup)
               .WithMany(e => e.Additives)
               .HasForeignKey(e => e.ProductSetupId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Amount)
               .IsRequired()
               .HasComment("Describes the amount of the additive type.");

        builder.Property(p => p.IsProductDependent)
               .IsRequired()
               .HasDefaultValue(false)
               .HasComment("Describes if the selected additive type is product dependend.");

        builder.HasIndex(a => new
        {
            a.ProductSetupId,
            a.AdditiveTypeId
        })
        .IsUnique();
    }
}
