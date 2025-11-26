using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;

/// <summary>
/// https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/relationships?redirectedfrom=MSDN
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseConfiguration<TEntity>(bool primaryIdNeeded) : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity, new()
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        if (primaryIdNeeded)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasComment("Auto generated id which provides uniqueness.");

            builder.HasIndex(x => x.Id);
        }
        else
        {
            builder.Ignore(x => x.Id);
        }

        builder.Property(p => p.CreatedAt)
            .HasComment("Represents the creation time of the record (row).")
            .IsRequired();

        builder.Property(p => p.CreatedById)
               .HasComment("Presents the creator of the record (row).")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasComment("Represents the update time of the record (row).")
            .IsRequired();

        builder.Property(p => p.UpdatedById)
            .HasComment("Presents the updater of the record (row).")
            .HasMaxLength(100)
            .IsRequired();
    }
}
