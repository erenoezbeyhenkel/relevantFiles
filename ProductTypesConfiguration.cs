using Hcb.Rnd.Pwn.Domain.Entities.Products;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Products;

public sealed class ProductTypesConfiguration() : BaseConfiguration<ProductType>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<ProductType> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("Describes the product type of the product. i.e: Liquid, Liquid concentrates, Solid, Predose Liquids...");

        builder.HasMany(pt => pt.Products)
               .WithOne(p => p.ProductType)
               .HasForeignKey(p => p.ProductTypeId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
                         new ProductType { Id = 1, Name = "Liquid", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 2, Name = "Liquid Concentrates", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 3, Name = "Solid", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 4, Name = "Solid Compacts", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 5, Name = "Predose Liquids", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 6, Name = "Predose Solid", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 7, Name = "Predose Hybrid", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 8, Name = "Predose Sheet", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                         new ProductType { Id = 9, Name = "Others", CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) }
                        );

    }
}
