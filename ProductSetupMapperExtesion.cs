using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ProductSetup;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class ProductSetupMapperExtesion
{
    public static ProductSetup ToEntity(this ProductSetupDto dto) => new()
    {
        Id = dto.Id,
        ExperimentId = dto.ExperimentId,
        ProductId = dto.ProductId,
        WashingMachineId = dto.WashingMachineId,
        WashingMachine = dto.WashingMachine?.ToEntity(),
        Position = dto.Position,
        DosageMl = dto.DosageMl,
        Density = dto.Density,
        DosageGram = dto.DosageGram,
        WaterHardness = dto.WaterHardness,
        WaterLevel = dto.WaterLevel,
        ChlorLevel = dto.ChlorLevel,
        MixingRatio = dto.MixingRatio,
        IsDiscardedForAi = dto.IsDiscardedForAi,
        IsDiscardedForReporting = dto.IsDiscardedForReporting,
        Pieces = dto.Pieces,
        RotationTime = dto.RotationTime,
        Drumspeed = dto.Drumspeed,
        ProcessInstructions = dto.ProcessInstructions,
        Additives = dto.ProductSetupAdditives?.Select(psa => psa?.ToEntity()).ToList()
    };

    public static ProductSetupDto ToDto(this ProductSetup entity) => new(entity.Id,
                                                                          entity.Product.Description,
                                                                          entity.Product.HugoProductDescription,
                                                                          entity.ExperimentId,
                                                                          entity.ProductId,
                                                                          $"{entity.Product.HugoWorksheetId} | {entity.Product.HugoProductId}",
                                                                          $"{entity.Product.HugoBatchId} | {entity.Product.HugoSampleId}",
                                                                          entity.Product.ProductType?.Name,
                                                                          entity.Product.ProductionType?.Name,
                                                                          entity.Position,
                                                                          entity.DosageMl,
                                                                          entity.Density,
                                                                          entity.DosageGram,
                                                                          entity.IsDiscardedForAi,
                                                                          entity.IsDiscardedForReporting,
                                                                          entity.Pieces,
                                                                          entity.WashingMachineId,
                                                                          entity.WashingMachine?.ToDto(),
                                                                          entity.WaterHardness,
                                                                          entity.WaterLevel,
                                                                          entity.ChlorLevel,
                                                                          entity.MixingRatio,
                                                                          entity.RotationTime,
                                                                          entity.Drumspeed,
                                                                          entity.ProcessInstructions,
                                                                          entity.Additives?.Select(a => a?.ToDto())?.ToList());

    public static void Map(this ProductSetupDto dto, ProductSetup entity)
    {
        entity.ExperimentId = dto.ExperimentId;
        entity.ProductId = dto.ProductId;
        entity.WashingMachineId = dto.WashingMachineId;
        entity.Position = dto.Position;
        entity.DosageMl = dto.DosageMl;
        entity.Density = dto.Density;
        entity.DosageGram = dto.DosageGram;
        entity.WaterHardness = dto.WaterHardness;
        entity.WaterLevel = dto.WaterLevel;
        entity.ChlorLevel = dto.ChlorLevel;
        entity.MixingRatio = dto.MixingRatio;
        entity.IsDiscardedForAi = dto.IsDiscardedForAi;
        entity.IsDiscardedForReporting = dto.IsDiscardedForReporting;
        entity.Pieces = dto.Pieces;
        entity.RotationTime = dto.RotationTime;
        entity.Drumspeed = dto.Drumspeed;
        entity.ProcessInstructions = dto.ProcessInstructions;
    }
}