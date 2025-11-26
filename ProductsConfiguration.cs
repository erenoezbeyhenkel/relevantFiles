using Hcb.Rnd.Pwn.Domain.Entities.Products;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Products;

public sealed class ProductsConfiguration() : BaseConfiguration<Product>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.OrderId).IsRequired();

        builder.HasOne(e => e.Order)
               .WithMany(e => e.Products)
               .HasForeignKey(e => e.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.HugoWorksheetId)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("This comes from Hugo Labvantage: worksheetid");

        builder.Property(p => p.HugoProductId)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("This comes from Hugo Labvantage. Formulation Id from PLM (Product Lifecylce Management): productid");

        builder.Property(p => p.HugoBatchId)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("This comes from Hugo Labvantage. batchid");

        builder.Property(p => p.HugoSampleId)
               .IsRequired()
               .HasMaxLength(40)
               .HasComment("This comes from Hugo Labvantage. s_sapmleid");

        builder.Property(p => p.HugoProductDescription)
               .IsRequired()
               .HasMaxLength(255)
               .HasComment("This comes from Hugo Labvantage. u_productdesc");

        builder.Property(p => p.HugoEventDt)
               .HasComment("This comes from Hugo Labvantage. eventdt");

        builder.Property(p => p.HugoStudyId)
               .HasMaxLength(40)
               .HasComment("This comes from Hugo Labvantage: studyid");

        builder.Property(p => p.HugoConditionLabel)
               .HasMaxLength(80)
               .HasComment("This comes from Hugo Labvantage: conditionlabel");

        builder.Property(p => p.HugoScheduleRuleLabel)
               .HasMaxLength(80)
               .HasComment("This comes from Hugo Labvantage: schedulerulelabel");

        builder.Property(p => p.Description)
               .IsRequired()
               .HasMaxLength(255)
               .HasComment("Internal naming for the sample.");



    }
}
