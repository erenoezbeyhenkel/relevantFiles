using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Monitor = Hcb.Rnd.Pwn.Domain.Entities.Monitors.Monitor;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Monitors;

public sealed class MonitorsConfiguration() : BaseConfiguration<Monitor>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<Monitor> builder)
    {
        base.Configure(builder);

       builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Name of the monitor.");

        builder.Property(m => m.IsActive)
            .IsRequired()
            .HasComment("Describes the status of the monitor. Active or Inactive");


        builder.Property(m => m.Acronym)
               .HasMaxLength(5)
               .HasComment("Abbreviation of the monitor. In generel used for Soar Production");

        builder.Property(m => m.Column)
            .IsRequired()
              .HasComment("A monitor is defined as a matrix. Column describes the number of horizontal places.");

        builder.Property(m => m.Row)
            .IsRequired()
              .HasComment("A monitor is defined as a matrix. Row describes the number of vertical places.");


        builder.Property(m => m.Comment)
            .HasComment("Additional comments for monitor.");

        builder.Property(m => m.IsDmcAvailable)
            .IsRequired()
            .HasComment("Defines if Dmc available.");

        builder.HasMany(m => m.MonitorSubstrates)
               .WithOne(ms => ms.Monitor)
               .HasForeignKey(ms => ms.MonitorId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(pt => pt.MonitorSetups)
               .WithOne(p => p.Monitor)
               .HasForeignKey(p => p.MonitorId)
               .OnDelete(DeleteBehavior.NoAction);


    }
}
