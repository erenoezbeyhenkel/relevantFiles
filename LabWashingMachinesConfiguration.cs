using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.FluentApi.EntityTypeConfigurations.Experiments;

public sealed class LabWashingMachinesConfiguration() : BaseConfiguration<LabWashingMachine>(primaryIdNeeded: true)
{
    public override void Configure(EntityTypeBuilder<LabWashingMachine> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.LocationLabel)
              .HasMaxLength(40);

        builder.Property(p => p.InstrumentModelId)
            .IsRequired()
            .HasMaxLength(225);

        builder.Property(p => p.InstrumentId)
            .HasMaxLength(40);

        builder.Property(p => p.InstrumentDescription)
            .HasMaxLength(255);

        builder.Property(p => p.InstrumentActiveFlag)
            .HasMaxLength(1);

        builder.Property(p => p.AdditionalInfo)
            .HasMaxLength(255);

        builder.Property(p => p.AssetNumber)
            .HasMaxLength(80);

        builder.Property(p => p.CostCenter)
            .HasMaxLength(20);

        builder.Property(p => p.CurrentLocation)
            .HasMaxLength(80);

        builder.Property(p => p.FactoryNumber)
            .HasMaxLength(40);

        builder.Property(p => p.LocationId)
            .HasMaxLength(20);

        builder.Property(p => p.MacAddress)
            .HasMaxLength(40);

        builder.Property(p => p.RndOrderNumber)
            .HasMaxLength(40);

        builder.Property(p => p.SerialNumber)
            .HasMaxLength(40);

        builder.Property(p => p.WorkshopDeviceNumber)
            .HasMaxLength(80);

        builder.Property(p => p.IsInHdpDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.CreatedDateInstrument)
            .IsRequired();

        builder.Property(p => p.ModifiedDateInstrument)
        .IsRequired();

        //builder.HasData(new LabWashingMachine { Id = 1, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "1", InstrumentId = "W 25/01 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000001", MacAddress = "D3:F5:90:28:4E:B1", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 2, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "2", InstrumentId = "W 25/02 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000002", MacAddress = "G4:F5:90:28:4E:23", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 3, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "3", InstrumentId = "W 25/03 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000003", MacAddress = "H6:F5:90:18:4A:56", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 4, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "4", InstrumentId = "W 25/04 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000004", MacAddress = "L9:F5:90:16:4O:84", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 5, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "5", InstrumentId = "W 25/05 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000005", MacAddress = "C5:F5:90:23:4U:24", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 6, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "6", InstrumentId = "W 25/06 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000006", MacAddress = "Y9:F5:90:54:3E:12", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 7, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "7", InstrumentId = "W 25/07 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000007", MacAddress = "E4:F5:90:98:4E:89", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 8, InstrumentModelId = "Miele - WCI 360 WTL", LocationLabel = "8", InstrumentId = "W 25/08 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/01", FactoryNumber = "Factory Number", LocationId = "L-00000008", MacAddress = "Y2:F5:90:31:6E:12", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
                        
        //                new LabWashingMachine { Id = 10, InstrumentModelId = "Miele - W 1514", LocationLabel = "1", InstrumentId = "W 25/01 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000001", MacAddress = "Z3:F5:12:28:4E:B1", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 11, InstrumentModelId = "Miele - W 1514", LocationLabel = "2", InstrumentId = "W 25/02 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000002", MacAddress = "G4:F5:90:28:4E:23", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 12, InstrumentModelId = "Miele - W 1514", LocationLabel = "3", InstrumentId = "W 25/03 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000003", MacAddress = "H6:F5:43:18:4A:56", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 13, InstrumentModelId = "Miele - W 1514", LocationLabel = "4", InstrumentId = "W 25/04 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000004", MacAddress = "M9:I9:35:16:4O:84", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 14, InstrumentModelId = "Miele - W 1514", LocationLabel = "5", InstrumentId = "W 25/05 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000005", MacAddress = "K8:F5:74:23:4U:24", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 15, InstrumentModelId = "Miele - W 1514", LocationLabel = "6", InstrumentId = "W 25/06 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000006", MacAddress = "X9:F5:45:54:3E:12", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 16, InstrumentModelId = "Miele - W 1514", LocationLabel = "7", InstrumentId = "W 25/07 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000007", MacAddress = "E4:P5:34:9J:4E:89", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) },
        //                new LabWashingMachine { Id = 17, InstrumentModelId = "Miele - W 1514", LocationLabel = "8", InstrumentId = "W 25/08 TestRIoTS", InstrumentDescription = "Default Miele - WCI 360 WTL Instrument Description", InstrumentActiveFlag = 'Y', AdditionalInfo = "Default additional info", AssetNumber = "Asset Number", CostCenter = "CostCenter", CurrentLocation = "DUS / B14 /0135/02", FactoryNumber = "Factory Number", LocationId = "R-00000008", MacAddress = "Y2:F5:68:31:6E:76", IsInHdpDeleted = false, CreatedDateInstrument = new DateTime(2024, 6, 14), ModifiedDateInstrument = new DateTime(2024, 6, 14), CreatedById = "SystemUser", CreatedAt = new DateTime(2024, 6, 14), UpdatedById = "SystemUser", UpdatedAt = new DateTime(2024, 6, 14) });
    }
}
