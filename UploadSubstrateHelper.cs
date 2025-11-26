using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;

public static class UploadSubstrateHelper
{
    public static List<SubstrateFamily> SetupSubstrateFamilies(this XLWorkbook workbook, IEnumerable<SubstrateCategory> substrateCategories, IEnumerable<ClusterType> clusterTypes)
    {
        var excelSubstrateFamilies = new List<SubstrateFamily>();

        var substrateFamilyWorksheet = workbook.Worksheets.First();
        var substrateFamilyWorksheetRows = substrateFamilyWorksheet.RangeUsed().RowsUsed();

        var headerRow = substrateFamilyWorksheetRows.First();
        var headers = headerRow.Cells().Select(c => c.Value.ToString()).ToList();

        foreach (var row in substrateFamilyWorksheetRows.Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var substrateFamily = new SubstrateFamily();
            foreach (var cell in row.Cells())
            {
                var header = headers[cell.Address.ColumnNumber - 1].Trim();

                if (Guard.Against.IsNullOrEmpty(header))
                    break;

                if (Guard.Control.IsEqual("Name", header) && Guard.Against.IsNullOrEmpty(cell.Value.ToString()))
                    break;

                if (Guard.Control.IsEqual("Name", header))
                    substrateFamily.Name = cell.Value.ToString();

                if (Guard.Control.IsEqual("SubstrateCategory", header))
                {
                    var category = substrateCategories.FirstOrDefault(c => c.Name == cell.Value.ToString());
                    if (Guard.Against.IsNull(category))
                        break;

                    substrateFamily.SubstrateCategoryId = category.Id;
                }

                if (Guard.Control.IsEqual("Bleach", header) && !Guard.Against.IsNullOrEmpty(cell.Value.ToString()))
                {
                    var clusterType = clusterTypes.FirstOrDefault(c => c.Name == "Bleach");
                    substrateFamily.SubstrateFamilyClusterTypes.Add(new SubstrateFamilyClusterType
                    {
                        ClusterTypeId = clusterType.Id,
                    });
                }

                if (Guard.Control.IsEqual("Enzyme", header) && !Guard.Against.IsNullOrEmpty(cell.Value.ToString()))
                {
                    var clusterType = clusterTypes.FirstOrDefault(c => c.Name == "Enzyme");
                    substrateFamily.SubstrateFamilyClusterTypes.Add(new SubstrateFamilyClusterType
                    {
                        ClusterTypeId = clusterType.Id,
                    });
                }

                if (Guard.Control.IsEqual("Surfactant", header) && !Guard.Against.IsNullOrEmpty(cell.Value.ToString()))
                {
                    var clusterType = clusterTypes.FirstOrDefault(c => c.Name == "Surfactant");
                    substrateFamily.SubstrateFamilyClusterTypes.Add(new SubstrateFamilyClusterType
                    {
                        ClusterTypeId = clusterType.Id,
                    });
                }

                if (Guard.Control.IsEqual("Mechanic", header) && !Guard.Against.IsNullOrEmpty(cell.Value.ToString()))
                {
                    var clusterType = clusterTypes.FirstOrDefault(c => c.Name == "Mechanic");
                    substrateFamily.SubstrateFamilyClusterTypes.Add(new SubstrateFamilyClusterType
                    {
                        ClusterTypeId = clusterType.Id,
                    });
                }
            }
            excelSubstrateFamilies.Add(substrateFamily);
        }

        excelSubstrateFamilies.RemoveAll(sf => Guard.Against.IsNullOrEmpty(sf.Name));
        return excelSubstrateFamilies;
    }

    public static void SetupSubstrates(this List<SubstrateFamily> substrateFamilies, XLWorkbook workbook, IEnumerable<FabricComposition> fabricCompositions, IEnumerable<ProductionMode> productionModes)
    {
        var substratesWorksheet = workbook.Worksheets.Skip(1).First();
        var substratesWorksheetRows = substratesWorksheet.RangeUsed().RowsUsed();

        var substratesHeaderRow = substratesWorksheetRows.First();
        var substratesHeaders = substratesHeaderRow.Cells().Select(c => c.Value.ToString()).ToList();

        foreach (var row in substratesWorksheetRows.Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var substrate = new Substrate();
            foreach (var cellS in row.Cells())
            {
                var header = substratesHeaders[cellS.Address.ColumnNumber - 1].Trim();

                if (Guard.Against.IsNullOrEmpty(header))
                    break;

                if (Guard.Control.IsEqual("SubstrateFamily", header) && Guard.Against.IsNullOrEmpty(cellS.Value.ToString()))
                    break;

                if (Guard.Control.IsEqual("Name", header) && Guard.Against.IsNullOrEmpty(cellS.Value.ToString()))
                    break;

                if (Guard.Control.IsEqual("SubstrateFamily", header))
                {
                    var labelIdentifierCell = row.Cells().FirstOrDefault(c => c.Address.ColumnNumber == cellS.Address.ColumnNumber + 2);
                    if (DoesSubstrateExistInMappingSheet(labelIdentifierCell.Value.ToString(), workbook.Worksheets.Skip(3).First()))
                    {
                        var substrateFamily = substrateFamilies.FirstOrDefault(sf => sf.Name == cellS.Value.ToString());
                        if (!Guard.Against.IsNull(substrateFamily))
                            substrateFamily.Substrates.Add(substrate);
                    }
                }

                if (Guard.Control.IsEqual("Label", header))
                    substrate.Label = cellS.Value.ToString();

                if (Guard.Control.IsEqual("Name", header))
                    substrate.Name = cellS.Value.ToString();

                if (Guard.Control.IsEqual("CO %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "CO");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("CV %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "CV");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("PES %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "PES");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("PA %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "PA");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("PAN %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "PAN");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("EL %", header) && !Guard.Against.IsNull(cellS.Value) && !cellS.Value.IsBlank)
                {
                    var fabricComposition = fabricCompositions.FirstOrDefault(c => c.Name == "EL");
                    if (!Guard.Against.IsNull(fabricComposition))
                    {
                        substrate.SubstrateFabricCompositions.Add(new SubstrateFabricComposition
                        {
                            Percentage = cellS.Value.GetNumber().ToDecimal(),
                            FabricCompositionId = fabricComposition.Id,
                            FabricComposition = fabricComposition
                        });
                    }
                }

                if (Guard.Control.IsEqual("Production Mode", header))
                {
                    var productionMode = productionModes.FirstOrDefault(pm => pm.Name == cellS.Value.ToString());
                    if (!Guard.Against.IsNull(productionMode))
                        substrate.ProductionModeId = productionMode.Id;
                }

                if (Guard.Control.IsEqual("Comment", header))
                    substrate.Comment = cellS.Value.ToString();
            }
        }
    }

    public static List<Domain.Entities.Monitors.Monitor> SetupMonitors(this XLWorkbook workbook, IEnumerable<MonitorType> monitorTypes)
    {
        var excelMonitors = new List<Domain.Entities.Monitors.Monitor>();

        var monitorsWorksheet = workbook.Worksheets.Skip(2).First();
        var monitorsWorksheetRows = monitorsWorksheet.RangeUsed().RowsUsed();

        var monitorsHeaderRow = monitorsWorksheetRows.First();
        var monitorsHeaders = monitorsHeaderRow.Cells().Select(c => c.Value.ToString()).ToList();

        foreach (var row in monitorsWorksheetRows.Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var monitor = new Domain.Entities.Monitors.Monitor();
            foreach (var cellS in row.Cells())
            {
                var header = monitorsHeaders[cellS.Address.ColumnNumber - 1].Trim();

                if (Guard.Against.IsNullOrEmpty(header))
                    break;

                if (Guard.Control.IsEqual("Name", header))
                    monitor.Name = cellS.Value.ToString();

                if (Guard.Control.IsEqual("IsActive", header))
                    monitor.IsActive = cellS.Value.GetNumber().ToInt() == 1;

                if (Guard.Control.IsEqual("Acronym", header))
                    monitor.Acronym = cellS.Value.ToString();

                if (Guard.Control.IsEqual("MonitorType", header))
                {
                    var monitorType = monitorTypes.FirstOrDefault(sf => sf.Name == cellS.Value.ToString());
                    if (!Guard.Against.IsNull(monitorType))
                    {
                        monitor.MonitorTypeId = monitorType.Id;
                    }
                }

                if (Guard.Control.IsEqual("Column", header))
                    monitor.Column = cellS.Value.GetNumber().ToInt();

                if (Guard.Control.IsEqual("Row", header))
                    monitor.Row = cellS.Value.GetNumber().ToInt();

                if (Guard.Control.IsEqual("Comment", header))
                    monitor.Comment = cellS.Value.ToString();

                if (Guard.Control.IsEqual("IsDmcAvailable", header))
                    monitor.IsDmcAvailable = !Guard.Against.IsNullOrEmpty(cellS.Value.ToString()) && cellS.Value.GetNumber().ToInt() == 1;

            }

            excelMonitors.Add(monitor);
        }

        return excelMonitors;
    }

    public static void SetupMonitorSubstrates(this List<Domain.Entities.Monitors.Monitor> excelMonitors, IEnumerable<Substrate> excelSubstrates, IEnumerable<Substrate> dbSubstrates, XLWorkbook workbook)
    {
        var monitorSubstratesWorksheet = workbook.Worksheets.Skip(3).First();
        var monitorSubstratesWorksheetRows = monitorSubstratesWorksheet.RangeUsed().RowsUsed();

        var monitorSubstratesHeaderRow = monitorSubstratesWorksheetRows.First();
        var monitorSubstratesHeaders = monitorSubstratesHeaderRow.Cells().Select(c => c.Value.ToString()).ToList();

        foreach (var row in monitorSubstratesWorksheetRows.Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var monitorSubstrate = new MonitorSubstrate();
            foreach (var cellS in row.Cells())
            {
                var header = monitorSubstratesHeaders[cellS.Address.ColumnNumber - 1].Trim();

                if (Guard.Against.IsNullOrEmpty(header))
                    break;

                if (Guard.Against.IsNullOrEmpty(cellS.Value.ToString()))
                    break;

                if (Guard.Control.IsEqual("SubstrateIdentifier", header))
                {
                    //dbSubstrateFirst
                    var substrate = dbSubstrates.FirstOrDefault(s => s.GetSubstrateIdentifier() == cellS.Value.ToString());
                    if (Guard.Against.IsNull(substrate))
                        substrate = excelSubstrates.FirstOrDefault(s => s.GetSubstrateIdentifier() == cellS.Value.ToString());

                    if (!Guard.Against.IsNull(substrate))
                        monitorSubstrate.SubstrateId = substrate.Id;

                }

                if (Guard.Control.IsEqual("Position", header))
                    monitorSubstrate.Position = cellS.Value.GetNumber().ToInt();

                if (Guard.Control.IsEqual("Column", header))
                    monitorSubstrate.Column = cellS.Value.GetNumber().ToInt();

                if (Guard.Control.IsEqual("Row", header))
                    monitorSubstrate.Row = cellS.Value.GetNumber().ToInt();

                if (Guard.Control.IsEqual("Monitor", header))
                {
                    var excelMonitor = excelMonitors.FirstOrDefault(sf => sf.Name == cellS.Value.ToString());
                    if (!Guard.Against.IsNull(excelMonitor))
                    {
                        monitorSubstrate.MonitorId = excelMonitor.Id;
                        excelMonitor.MonitorSubstrates.Add(monitorSubstrate);
                    }
                }
            }
        }
    }

    private static bool DoesSubstrateExistInMappingSheet(string labelIdentifier, IXLWorksheet mapMonitorSubstratesSheet)
    {
        var monitorSubstratesWorksheetRows = mapMonitorSubstratesSheet.RangeUsed().RowsUsed();

        var monitorSubstratesHeaderRow = monitorSubstratesWorksheetRows.First();
        var monitorSubstratesHeaders = monitorSubstratesHeaderRow.Cells().Select(c => c.Value.ToString()).ToList();

        foreach (var row in monitorSubstratesWorksheetRows.Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var monitorSubstrate = new MonitorSubstrate();
            foreach (var cellS in row.Cells())
            {
                var header = monitorSubstratesHeaders[cellS.Address.ColumnNumber - 1].Trim();

                if (Guard.Against.IsNullOrEmpty(header))
                    break;

                if (Guard.Against.IsNullOrEmpty(cellS.Value.ToString()))
                    break;

                if (Guard.Control.IsEqual("SubstrateIdentifier", header))
                {
                    var result = Guard.Control.IsEqual(labelIdentifier.Trim(), cellS.Value.ToString().Trim());
                    if (result)
                        return result;
                }

            }
        }
        return false;
    }
}
