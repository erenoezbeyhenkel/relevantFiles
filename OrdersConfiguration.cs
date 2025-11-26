using Hcb.Rnd.Pwn.Domain.Entities.Orders;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Orders;

public sealed class OrdersConfiguration() : BaseConfiguration<Order>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);

        builder.Property(w => w.Description)
               .IsRequired()
               .HasComment("Decription of wash order.");

        builder.Property(w => w.HugoProjectId)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("This id comes from Hugo Labvantage rest api endpoint: dbo.formulationproject.formulationprojectid");

        builder.Property(w => w.InternalId)
               .IsRequired()
               .HasMaxLength(14)
               .HasComment("Internal id is the combination of the W-Year-Month-#Count. Count means the number of the wash order of the month. (W-YYYY-MM-NNNN)");

        builder.Property(w => w.ProductDeveloperAadGroupId)
               .IsRequired()
               .HasComment("Aad group Id of the order.");

        builder.HasOne(e => e.ProductDeveloperAadGroup)
           .WithMany(a => a.Orders)
           .HasForeignKey(e => e.ProductDeveloperAadGroupId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
