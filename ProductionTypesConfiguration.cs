using Hcb.Rnd.Pwn.Domain.Entities.Products;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Products;

public sealed class ProductionTypesConfiguration() : BaseConfiguration<ProductionType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ProductionType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the production type of the product. i.e: Fresh, Storage");

        builder.HasMany(pt => pt.Products)
               .WithOne(p => p.ProductionType)
               .HasForeignKey(p => p.ProductionTypeId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(new ProductionType { Id = 1, Name = "Fresh", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) }, 
                        new ProductionType { Id = 2, Name = "Storage", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}
