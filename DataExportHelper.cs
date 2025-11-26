using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetWashRunData;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Monitor = Hcb.Rnd.Pwn.Domain.Entities.Monitors.Monitor;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;

public static class DataExportHelper
{
    public static void AddMonitorsWorksheet(this XLWorkbook workbook, IEnumerable<Monitor> monitors)
    {
        var worksheet = workbook.Worksheets.Add("Monitors");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 2).Style.Font.Bold = true;
        worksheet.Cell(1, 3).Value = "IsActive";
        worksheet.Cell(1, 3).Style.Font.Bold = true;
        worksheet.Cell(1, 4).Value = "Acronym";
        worksheet.Cell(1, 4).Style.Font.Bold = true;
        worksheet.Cell(1, 5).Value = "Column";
        worksheet.Cell(1, 5).Style.Font.Bold = true;
        worksheet.Cell(1, 6).Value = "Row";
        worksheet.Cell(1, 6).Style.Font.Bold = true;
        worksheet.Cell(1, 7).Value = "IsDmcAvailable";
        worksheet.Cell(1, 7).Style.Font.Bold = true;

        int row = 2;
        foreach (var monitor in monitors)
        {
            worksheet.Cell(row, 1).Value = monitor.Id;
            worksheet.Cell(row, 2).Value = monitor.Name;
            worksheet.Cell(row, 3).Value = monitor.IsActive;
            worksheet.Cell(row, 4).Value = monitor.Acronym;
            worksheet.Cell(row, 5).Value = monitor.Column;
            worksheet.Cell(row, 6).Value = monitor.Row;
            worksheet.Cell(row, 7).Value = monitor.IsDmcAvailable;
            row++;
        }

        worksheet.AlternateRowColors();
    }

    public static void AddMonitorSubstratesWorksheet(this XLWorkbook workbook, IEnumerable<MonitorSubstrate> monitorSubstrates)
    {
        var worksheet = workbook.Worksheets.Add("MonitorSubstrates");

        worksheet.Cell(1, 1).Value = "Position";
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 2).Value = "Column";
        worksheet.Cell(1, 2).Style.Font.Bold = true;
        worksheet.Cell(1, 3).Value = "Row";
        worksheet.Cell(1, 3).Style.Font.Bold = true;
        worksheet.Cell(1, 4).Value = "MonitorId";
        worksheet.Cell(1, 4).Style.Font.Bold = true;
        worksheet.Cell(1, 5).Value = "MonitorName";
        worksheet.Cell(1, 5).Style.Font.Bold = true;
        worksheet.Cell(1, 6).Value = "SubstrateId";
        worksheet.Cell(1, 6).Style.Font.Bold = true;
        worksheet.Cell(1, 7).Value = "SubstrateName";
        worksheet.Cell(1, 7).Style.Font.Bold = true;

        int row = 2;
        foreach (var monitorSubstrate in monitorSubstrates)
        {
            worksheet.Cell(row, 1).Value = monitorSubstrate.Position;
            worksheet.Cell(row, 2).Value = monitorSubstrate.Column;
            worksheet.Cell(row, 3).Value = monitorSubstrate.Row;
            worksheet.Cell(row, 4).Value = monitorSubstrate.MonitorId;
            worksheet.Cell(row, 5).Value = monitorSubstrate.Monitor?.Name;
            worksheet.Cell(row, 6).Value = monitorSubstrate.SubstrateId;
            worksheet.Cell(row, 7).Value = monitorSubstrate.Substrate?.Name;
            row++;
        }

        worksheet.AlternateRowColors();
    }

    public static void AddSubstratesWorksheet(this XLWorkbook workbook, IEnumerable<Substrate> substrates)
    {
        var worksheet = workbook.Worksheets.Add("Substrates");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 2).Style.Font.Bold = true;
        worksheet.Cell(1, 3).Value = "HugoReference";
        worksheet.Cell(1, 3).Style.Font.Bold = true;
        worksheet.Cell(1, 4).Value = "ReleaseYear";
        worksheet.Cell(1, 4).Style.Font.Bold = true;
        worksheet.Cell(1, 5).Value = "SoarStainingMaterialDefinitionFileName";
        worksheet.Cell(1, 5).Style.Font.Bold = true;
        worksheet.Cell(1, 6).Value = "SubstrateFamilyId";
        worksheet.Cell(1, 6).Style.Font.Bold = true;
        worksheet.Cell(1, 7).Value = "SubstrateFamilyName";
        worksheet.Cell(1, 7).Style.Font.Bold = true;
        worksheet.Cell(1, 8).Value = "FabricTypeId";
        worksheet.Cell(1, 8).Style.Font.Bold = true;
        worksheet.Cell(1, 9).Value = "FabricTypeName";
        worksheet.Cell(1, 9).Style.Font.Bold = true;
        worksheet.Cell(1, 10).Value = "ProductionModeId";
        worksheet.Cell(1, 10).Style.Font.Bold = true;
        worksheet.Cell(1, 11).Value = "ProductionModeName";
        worksheet.Cell(1, 11).Style.Font.Bold = true;

        int row = 2;
        foreach (var substrate in substrates)
        {
            worksheet.Cell(row, 1).Value = substrate.Id;
            worksheet.Cell(row, 2).Value = substrate.Name;
            worksheet.Cell(row, 3).Value = substrate.HugoReference;
            worksheet.Cell(row, 4).Value = substrate.ReleaseYear;
            worksheet.Cell(row, 5).Value = substrate.SoarStainingMaterialDefinitionFileName;
            worksheet.Cell(row, 6).Value = substrate.SubstrateFamilyId;
            worksheet.Cell(row, 7).Value = substrate.SubstrateFamily?.Name;
            worksheet.Cell(row, 8).Value = substrate.FabricTypeId;
            worksheet.Cell(row, 9).Value = substrate.FabricType?.Name;
            worksheet.Cell(row, 10).Value = substrate.ProductionModeId;
            worksheet.Cell(row, 11).Value = substrate.ProductionMode?.Name;

            row++;
        }

        worksheet.AlternateRowColors();
    }

    public static void AddSubstrateFamiliesWorksheet(this XLWorkbook workbook, IEnumerable<SubstrateFamily> substrateFamilies)
    {
        var worksheet = workbook.Worksheets.Add("SubstrateFamilies");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 1).Style.Font.Bold = true;
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 2).Style.Font.Bold = true;
        worksheet.Cell(1, 3).Value = "SubstrateCategoryId";
        worksheet.Cell(1, 3).Style.Font.Bold = true;
        worksheet.Cell(1, 4).Value = "SubstrateCategoryName";
        worksheet.Cell(1, 4).Style.Font.Bold = true;
        worksheet.Cell(1, 5).Value = "ClusterTypeId";
        worksheet.Cell(1, 5).Style.Font.Bold = true;
        worksheet.Cell(1, 6).Value = "ClusterTypeName";
        worksheet.Cell(1, 6).Style.Font.Bold = true;


        int row = 2;
        foreach (var substrateFamily in substrateFamilies)
        {
            worksheet.Cell(row, 1).Value = substrateFamily.Id;
            worksheet.Cell(row, 2).Value = substrateFamily.Name;
            worksheet.Cell(row, 3).Value = substrateFamily.SubstrateCategoryId;
            worksheet.Cell(row, 4).Value = substrateFamily.SubstrateCategory?.Name;

            foreach (var substrateFamilyClusterType in substrateFamily.SubstrateFamilyClusterTypes ?? [])
            {
                worksheet.Cell(row, 5).Value = substrateFamilyClusterType.ClusterType?.Id;
                worksheet.Cell(row, 6).Value = substrateFamilyClusterType.ClusterType?.Name;
                row++;
            }
            row++;
        }

        worksheet.AlternateRowColors();
    }

    private static void AlternateRowColors(this IXLWorksheet worksheet)
    {
        var usedRange = worksheet.RangeUsed();
        if (!Guard.Against.IsNull(usedRange))
        {
            var numRows = usedRange.RowCount();
            for (int i = 1; i <= numRows; i++)
            {
                if (i % 2 == 0)
                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.LightBlue; // Light Blue color for even rows
                else
                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.White; // White color for odd rows
            }
        }
    }

    public static void AddExperimentSettingsWorksheet(this XLWorkbook workbook, ExperimentDto experiment, OrderDto order)
    {
        var experimentSettingsWorksheet = workbook.Worksheet("ExperimentSettings");
        experimentSettingsWorksheet.Cell(1, 1).Value = "ExperimentID";
        experimentSettingsWorksheet.Cell(1, 2).Value = experiment.Id;

        experimentSettingsWorksheet.Cell(2, 1).Value = "CreatedBy";
        experimentSettingsWorksheet.Cell(2, 2).Value = experiment.CreatedBy;

        experimentSettingsWorksheet.Cell(3, 1).Value = "Experiment";
        experimentSettingsWorksheet.Cell(3, 2).Value = $"Id: {experiment.Id} | Type: {experiment.ExperimentType.Name} | Repetition: {experiment.NumberOfRepetition}";

        experimentSettingsWorksheet.Cell(4, 1).Value = "Order";
        experimentSettingsWorksheet.Cell(4, 2).Value = $"{order.ProductDeveloperAadGroup.Name} | {order.Description}";

        experimentSettingsWorksheet.Cell(5, 1).Value = "Experiment Description";
        experimentSettingsWorksheet.Cell(5, 2).Value = experiment.Description ?? "-";

        experimentSettingsWorksheet.Cell(6, 1).Value = "Experiment Comment";
        experimentSettingsWorksheet.Cell(6, 2).Value = experiment.Comment ?? "-";

        experimentSettingsWorksheet.Hide();
    }

    public static void AddMonitorSetupsWorksheet(this XLWorkbook workbook, ExperimentDto experiment)
    {
        var monitorSetupsWorksheet = workbook.Worksheet("MonitorSetups");

        var monitorSetupsRow = 1;
        foreach (var monitorSetup in experiment.MonitorSetups)
        {
            monitorSetupsWorksheet.Cell(monitorSetupsRow, 1).Value = monitorSetup.MonitorName;
            monitorSetupsWorksheet.Cell(monitorSetupsRow, 2).Value = monitorSetup.IsPrewashed ? "prewashed" : string.Empty;
            monitorSetupsRow++;
        }

        monitorSetupsWorksheet.Hide();
    }

    public static void AddProductSetupsWorksheet(this XLWorkbook workbook, ExperimentDto experiment)
    {
        var productSetupsWorksheet = workbook.Worksheet("ProductSetups");
        var productSetupsRow = 1;
        foreach (var productSetup in experiment.ProductSetups.OrderBy(ps => ps.Position))
        {
            productSetupsWorksheet.Cell(productSetupsRow, 1).Value = productSetup.Position.GetFormattedPosition();
            productSetupsWorksheet.Cell(productSetupsRow, 2).Value = productSetup.Description;
            productSetupsWorksheet.Cell(productSetupsRow, 3).Value = $"- Water H: {productSetup.WaterHardness}\n - Chlor L: {productSetup.ChlorLevel}\n - Water L: {productSetup.WaterLevel}\n - Mixing R: {productSetup.MixingRatio}";
            productSetupsWorksheet.Cell(productSetupsRow, 4).Value = Math.Round(productSetup.DosageGram, 2);
            productSetupsWorksheet.Cell(productSetupsRow, 5).Value = string.Join("\n", productSetup.ProductSetupAdditives.Select(psa => $"- {psa.AdditiveName}"));
            productSetupsWorksheet.Cell(productSetupsRow, 6).Value = $"{productSetup.WashingMachine.Name}\n {productSetup.WashingMachine.Program}\n{productSetup.WashingMachine.Temperature} [°C]";
            productSetupsWorksheet.Cell(productSetupsRow, 7).Value = productSetup.ProcessInstructions;
            productSetupsRow++;
        }

        productSetupsWorksheet.Hide();
    }

    public static void TidyUpExperimentOverviewWorksheet(this XLWorkbook workbook)
    {
        var experimentOverviewWorksheet = workbook.Worksheet("Experiment Overview");

        for (int r = 100; r > 9; r--)
        {
            if (r == 33 || r == 21)
                continue;

            var row = experimentOverviewWorksheet.Row(r);
            if (row.IsRowTrulyEmpty())
                row.Delete();
        }
        experimentOverviewWorksheet.Protect();
    }

    public static void AddSynchronizeLabWashingMachinesWorksheet(this IXLWorksheet worksheet, List<LabWashingMachine> labWashingMachines)
    {
        if (!Guard.Against.IsAnyOrNotEmpty(labWashingMachines))
            return;

        int currentRow = 1;

        worksheet.Cell(currentRow, 1).Value = "InstrumentModelId";
        worksheet.Cell(currentRow, 2).Value = "LocationLabel";
        worksheet.Cell(currentRow, 3).Value = "InstrumentId";
        worksheet.Cell(currentRow, 4).Value = "InstrumentDescription";
        worksheet.Cell(currentRow, 5).Value = "InstrumentActiveFlag";
        worksheet.Cell(currentRow, 6).Value = "AdditionalInfo";
        worksheet.Cell(currentRow, 7).Value = "AssetNumber";
        worksheet.Cell(currentRow, 8).Value = "CostCenter";
        worksheet.Cell(currentRow, 9).Value = "CurrentLocation";
        worksheet.Cell(currentRow, 10).Value = "FactoryNumber";
        worksheet.Cell(currentRow, 11).Value = "LocationId";
        worksheet.Cell(currentRow, 12).Value = "MacAddress";
        worksheet.Cell(currentRow, 13).Value = "RndOrderNumber";
        worksheet.Cell(currentRow, 14).Value = "SerialNumber";
        worksheet.Cell(currentRow, 15).Value = "WorkshopDeviceNumber";
        worksheet.Cell(currentRow, 16).Value = "IsInHdpDeleted";
        worksheet.Cell(currentRow, 17).Value = "CreatedDateInstrument";
        worksheet.Cell(currentRow, 18).Value = "ModifiedDateInstrument";
        worksheet.Cell(currentRow, 19).Value = "CreatedDateLocation";
        worksheet.Cell(currentRow, 20).Value = "ModifiedDateLocation";
        worksheet.Cell(currentRow, 21).Value = "NextCheck";
        worksheet.Cell(currentRow, 22).Value = "EntryDate";
        worksheet.Cell(currentRow, 23).Value = "LastCheck";

        worksheet.Range(currentRow, 1, currentRow, 23).Style.Font.Bold = true;
        worksheet.Range(currentRow, 1, currentRow, 23).Style.Fill.BackgroundColor = XLColor.LightGray;

        foreach (var machine in labWashingMachines)
        {
            currentRow++;
            worksheet.Cell(currentRow, 1).Value = machine.InstrumentModelId;
            worksheet.Cell(currentRow, 2).Value = machine.LocationLabel;
            worksheet.Cell(currentRow, 3).Value = machine.InstrumentId;
            worksheet.Cell(currentRow, 4).Value = machine.InstrumentDescription;
            worksheet.Cell(currentRow, 5).Value = machine.InstrumentActiveFlag.ToString();
            worksheet.Cell(currentRow, 6).Value = machine.AdditionalInfo;
            worksheet.Cell(currentRow, 7).Value = machine.AssetNumber;
            worksheet.Cell(currentRow, 8).Value = machine.CostCenter;
            worksheet.Cell(currentRow, 9).Value = machine.CurrentLocation;
            worksheet.Cell(currentRow, 10).Value = machine.FactoryNumber;
            worksheet.Cell(currentRow, 11).Value = machine.LocationId;
            worksheet.Cell(currentRow, 12).Value = machine.MacAddress;
            worksheet.Cell(currentRow, 13).Value = machine.RndOrderNumber;
            worksheet.Cell(currentRow, 14).Value = machine.SerialNumber;
            worksheet.Cell(currentRow, 15).Value = machine.WorkshopDeviceNumber;
            worksheet.Cell(currentRow, 16).Value = machine.IsInHdpDeleted;
            worksheet.Cell(currentRow, 17).Value = machine.CreatedDateInstrument;
            worksheet.Cell(currentRow, 18).Value = machine.ModifiedDateInstrument;
            worksheet.Cell(currentRow, 19).Value = machine.CreatedDateLocation;
            worksheet.Cell(currentRow, 20).Value = machine.ModifiedDateLocation;
            worksheet.Cell(currentRow, 21).Value = machine.NextCheck;
            worksheet.Cell(currentRow, 22).Value = machine.EntryDate;
            worksheet.Cell(currentRow, 23).Value = machine.LastCheck;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.SheetView.FreezeRows(1);
        worksheet.AlternateRowColors();
    }

    public static void AddSynchronizeWashingMachinesWorksheet(this IXLWorksheet worksheet, List<WashingMachine> washingMachines)
    {
        if (!Guard.Against.IsAnyOrNotEmpty(washingMachines))
            return;

        int currentRow = 1;

        worksheet.Cell(currentRow, 1).Value = "Name";
        worksheet.Cell(currentRow, 2).Value = "Program";
        worksheet.Cell(currentRow, 3).Value = "Temperature";
        worksheet.Cell(currentRow, 4).Value = "Main Wash Time";
        worksheet.Cell(currentRow, 5).Value = "Total Wash Time";
        worksheet.Cell(currentRow, 6).Value = "Active Flag";
        worksheet.Cell(currentRow, 7).Value = "Drain Pump Equipped";
        worksheet.Cell(currentRow, 8).Value = "Loading Type";
        worksheet.Cell(currentRow, 9).Value = "Manufacturer";
        worksheet.Cell(currentRow, 10).Value = "Maximum Load";
        worksheet.Cell(currentRow, 11).Value = "Needs Hot Water";
        worksheet.Cell(currentRow, 12).Value = "Program List ID";
        worksheet.Cell(currentRow, 13).Value = "User Sequence";
        worksheet.Cell(currentRow, 14).Value = "Created Date (Instrument Model)";
        worksheet.Cell(currentRow, 15).Value = "Modified Date (Instrument Model)";
        worksheet.Cell(currentRow, 16).Value = "Created Date (Program List)";
        worksheet.Cell(currentRow, 17).Value = "Modified Date (Program List)";
        worksheet.Cell(currentRow, 18).Value = "Is In HDP Deleted";

        worksheet.Range(currentRow, 1, currentRow, 18).Style.Font.Bold = true;
        worksheet.Range(currentRow, 1, currentRow, 18).Style.Fill.BackgroundColor = XLColor.LightGray;

        foreach (var machine in washingMachines)
        {
            currentRow++;
            worksheet.Cell(currentRow, 1).Value = machine.Name;
            worksheet.Cell(currentRow, 2).Value = machine.Program;
            worksheet.Cell(currentRow, 3).Value = machine.Temperature;
            worksheet.Cell(currentRow, 4).Value = machine.MainWashTime;
            worksheet.Cell(currentRow, 5).Value = machine.TotalWashTime;
            worksheet.Cell(currentRow, 6).Value = machine.ActiveFlag.ToString();
            worksheet.Cell(currentRow, 7).Value = machine.DrainPumpEquipped.ToString();
            worksheet.Cell(currentRow, 8).Value = machine.LoadingType;
            worksheet.Cell(currentRow, 9).Value = machine.Manufacturer;
            worksheet.Cell(currentRow, 10).Value = machine.MaximumLoad;
            worksheet.Cell(currentRow, 11).Value = machine.NeedsHotWater.ToString();
            worksheet.Cell(currentRow, 12).Value = machine.ProgramListId;
            worksheet.Cell(currentRow, 13).Value = machine.UserSequence;
            worksheet.Cell(currentRow, 14).Value = machine.CreatedDateInstrumentModel;
            worksheet.Cell(currentRow, 15).Value = machine.ModifiedDateInstrumentModel;
            worksheet.Cell(currentRow, 16).Value = machine.CreatedDateProgramList;
            worksheet.Cell(currentRow, 17).Value = machine.ModifiedDateProgramList;
            worksheet.Cell(currentRow, 18).Value = machine.IsInHdpDeleted;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.SheetView.FreezeRows(1);
        worksheet.AlternateRowColors();
    }

    public static void AddSynchronizeLabWashingMachineMappingsWorksheet(this IXLWorksheet worksheet, List<LabWashingMachineMapping> labWashingMachineMappings)
    {
        if (!Guard.Against.IsAnyOrNotEmpty(labWashingMachineMappings))
            return;

        int currentRow = 1;

        worksheet.Cell(currentRow, 1).Value = "LabWashingMachineId";
        worksheet.Cell(currentRow, 2).Value = "WashingMachineId";

        worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
        worksheet.Range(currentRow, 1, currentRow, 2).Style.Fill.BackgroundColor = XLColor.LightGray;

        foreach (var mapping in labWashingMachineMappings)
        {
            currentRow++;
            worksheet.Cell(currentRow, 1).Value = mapping.LabWashingMachineId;
            worksheet.Cell(currentRow, 2).Value = mapping.WashingMachineId;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.SheetView.FreezeRows(1);
        worksheet.AlternateRowColors();
    }

    public static void ExportWashRunDataToExcel(this XLWorkbook workbook, List<WashRunDataDto> dataList)
    {
        var worksheet = workbook.Worksheets.Add("Wash Run Data");

        var properties = typeof(WashRunDataDto).GetProperties();

        // Add headers
        for (int i = 0; i < properties.Length; i++)
            worksheet.Cell(1, i + 1).Value = properties[i].Name;

        // Add data
        for (int row = 0; row < dataList.Count; row++)
        {
            var item = dataList[row];

            for (int col = 0; col < properties.Length; col++)
            {
                var prop = properties[col];
                var value = prop.GetValue(item);

                // Special handling for List<string>
                if (value is List<string> list)
                    worksheet.Cell(row + 2, col + 1).Value = string.Join(", ", list);
                else
                    worksheet.Cell(row + 2, col + 1).Value = value?.ToString() ?? "";
            }
        }

        // Optional: adjust column widths
        worksheet.Columns().AdjustToContents();
    }

}
