using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetWashRunData;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class WashRunMapperExtensions
{
    public static WashRunDataDto ToWashRunDataDto(this Experiment entity,
                                                  FrameMonitor frameMonitor,
                                                  ProductSetup productSetup,
                                                  WashingMachine washingMachine,
                                                  LabWashingMachine labWashingMachine) => new(frameMonitor.WashRun,
                                                                                              entity.OrderId.Value,
                                                                                              entity.Id,
                                                                                              entity.Description,
                                                                                              entity.ExperimentType.Name,
                                                                                              $"{frameMonitor.WashingMachineMacAddressMappingDate.ToUnixTimestampAsString()}000000",
                                                                                              frameMonitor.SubmitDate.ToUnixTimestampAsString(),
                                                                                              labWashingMachine.InstrumentModelId,
                                                                                              labWashingMachine.InstrumentId,
                                                                                              labWashingMachine.MacAddress,
                                                                                              labWashingMachine.InstrumentDescription,
                                                                                              labWashingMachine.AdditionalInfo,
                                                                                              Guard.Against.IsEqual(frameMonitor.ProductSetup.WashingMachine.DrainPumpEquipped, 'Y') ? "Yes" : "No",
                                                                                              washingMachine.LoadingType,
                                                                                              washingMachine.MaximumLoad,
                                                                                              Guard.Against.IsEqual(frameMonitor.ProductSetup.WashingMachine.NeedsHotWater, 'Y') ? "Yes" : "No",
                                                                                              washingMachine.ProgramListId,
                                                                                              $"{washingMachine.Program} - {washingMachine.Temperature} °C",
                                                                                              washingMachine.MainWashTime,
                                                                                              washingMachine.TotalWashTime,
                                                                                              labWashingMachine.AssetNumber,
                                                                                              labWashingMachine.CostCenter,
                                                                                              labWashingMachine.CurrentLocation,
                                                                                              labWashingMachine.LocationLabel,
                                                                                              labWashingMachine.EntryDate.ToUnixTimestampAsString(),
                                                                                              labWashingMachine.FactoryNumber,
                                                                                              labWashingMachine.LastCheck.ToUnixTimestampAsString(),
                                                                                              labWashingMachine.NextCheck.ToUnixTimestampAsString(),
                                                                                              labWashingMachine.RndOrderNumber,
                                                                                              labWashingMachine.SerialNumber,
                                                                                              labWashingMachine.WorkshopDeviceNumber,
                                                                                              frameMonitor.WashCycleSetup.WashCycle,
                                                                                              frameMonitor.RepetitionNumber,
                                                                                              productSetup.Position,
                                                                                              string.Empty,
                                                                                              productSetup.Id,
                                                                                              productSetup.DosageGram,
                                                                                              productSetup.ChlorLevel,
                                                                                              productSetup.MixingRatio,
                                                                                              productSetup.Product.HugoWorksheetId,
                                                                                              productSetup.Product.HugoProductId,
                                                                                              productSetup.Product.HugoBatchId,
                                                                                              productSetup.Product.HugoSampleId,
                                                                                              [.. productSetup.Additives.Select(a => a.AdditiveType?.Name)],
                                                                                              entity.MonitorSetups?.Select(ms => ms.Monitor?.Name).ToList());
}
