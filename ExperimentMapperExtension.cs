using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.AzureGroups;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ProductSetup;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.AzureGroups;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class ExperimentMapperExtension
{
    public static Experiment ToEntity(this ExperimentDto dto) => new()
    {
        Id = dto.Id,
        OrderId = dto.OrderId,
        ExperimentTypeId = dto.ExperimentTypeId,
        ExperimentType = dto.ExperimentType?.ToEntity(),
        ExperimentStatusId = dto.ExperimentStatusId,
        ExperimentStatus = dto.ExperimentStatus?.ToEntity(),
        StatusOpenDate = dto.StatusOpenDate,
        StatusValidationDate = dto.StatusValidationDate,
        StatusPreparationDate = dto.StatusPreparationDate,
        StatusMeasurementDate = dto.StatusMeasurementDate,
        StatusCompletedDate = dto.StatusCompletedDate,
        ProductDeliveryDate = dto.ProductDeliveryDate,
        StartingCalendarWeekDate = dto.StartingCalendarWeekDate,
        Description = dto.Description,
        Comment = dto.Comment,
        NumberOfRepetition = dto.NumberOfRepetition,
        IsActive = dto.IsActive,
        IsExternal = dto.IsExternal,
        TemplateExperimentId = dto.TemplateExperimentId,
        ValidatorAadGroupId = dto.ValidatorAadGroupId,
        ValidatorAadGroup = dto.ValidatorAadGroupDto?.ToEntity(),
        OperatorAadGroupId = dto.OperatorAadGroupId,
        OperatorAadGroup = dto.OperatorAadGroupDto?.ToEntity(),
        MonitorSetups = dto.MonitorSetups?.Select(ms => ms?.ToEntity())?.ToList(),
        ProductSetups = dto.ProductSetups?.Select(ps => ps?.ToEntity())?.ToList(),
        TemplateAdditives = dto.TemplateAdditives?.Select(ta => ta?.ToEntity())?.ToList(),
        RequestedProgram = dto.RequestedProgram,
        RequestedTemperature = dto.RequestedTemperature
    };

    public static Experiment ToEntity(this ExperimentTemplateDto dto) => new()
    {
        ExperimentTypeId = dto.ExperimentTypeId,
        NumberOfRepetition = dto.NumberOfRepetition,
        Description = dto.Description,
        IsActive = dto.IsActive,
        TemplateSetting = dto.TemplateSetting.ToEntity(),
        AssignedExperimentTemplateGroups = dto.AssignedExperimentTemplateGroupDtos?.Select(teaad => teaad.ToEntity()).ToList()
    };

    public static ExperimentTemplateDto ToExperimentTemplateDto(this Experiment entity, string createdByUserName, string updatedByUserName) => new(entity.Id,
                                                                                                                                                   entity.ExperimentTypeId,
                                                                                                                                                   entity.NumberOfRepetition,
                                                                                                                                                   entity.Description,
                                                                                                                                                   entity.IsActive,
                                                                                                                                                   createdByUserName,
                                                                                                                                                   updatedByUserName,
                                                                                                                                                   entity.TemplateSetting?.ToDto(),
                                                                                                                                                   [.. entity.AssignedExperimentTemplateGroups?.Select(aetg => aetg?.ToDto())]);


    public static ExperimentDto ToDto(this Experiment entity, string createdByUserName = default, string updatedByUserName = default) => new(entity.Id,
                                                                                                                                             entity.OrderId,
                                                                                                                                             entity.ExperimentTypeId,
                                                                                                                                             entity.Order?.ProductDeveloperAadGroup?.Name,
                                                                                                                                             entity.Order?.InternalId,
                                                                                                                                             entity.Order?.CreatedAt,
                                                                                                                                             entity.ExperimentType?.ToDto(),
                                                                                                                                             entity.ExperimentStatusId,
                                                                                                                                             entity.ExperimentStatus?.ToDto(),
                                                                                                                                             entity.StatusOpenDate,
                                                                                                                                             entity.StatusValidationDate,
                                                                                                                                             entity.StatusPreparationDate,
                                                                                                                                             entity.StatusMeasurementDate,
                                                                                                                                             entity.StatusCompletedDate,
                                                                                                                                             entity.ProductDeliveryDate,
                                                                                                                                             entity.StartingCalendarWeekDate,
                                                                                                                                             entity.Description,
                                                                                                                                             entity.Comment,
                                                                                                                                             entity.NumberOfRepetition,
                                                                                                                                             entity.IsActive,
                                                                                                                                             entity.IsExternal,
                                                                                                                                             entity.TemplateExperimentId,
                                                                                                                                             !Guard.Against.IsNull(entity.TemplateSetting) ? entity.TemplateSetting.Id : default,
                                                                                                                                             entity.TemplateSetting?.ToDto(),
                                                                                                                                             entity.ValidatorAadGroupId,
                                                                                                                                             entity.ValidatorAadGroup?.ToDto(),
                                                                                                                                             entity.OperatorAadGroupId,
                                                                                                                                             entity.OperatorAadGroup?.ToDto(),
                                                                                                                                             entity.RequestedProgram,
                                                                                                                                             entity.RequestedTemperature,
                                                                                                                                             entity.MonitorSetups?.Select(ms => ms?.ToDto())?.ToList(),
                                                                                                                                             entity.ProductSetups?.Select(ps => ps?.ToDto())?.ToList(),
                                                                                                                                             entity.TemplateAdditives?.Select(ta => ta?.ToDto())?.ToList(),
                                                                                                                                             entity.AssignedExperimentTemplateGroups?.Select(teag => teag?.ToDto())?.ToList(),
                                                                                                                                             createdByUserName,
                                                                                                                                             updatedByUserName);

    public static ExperimentTemplateSummaryDto ToDto(this Experiment entity, IEnumerable<ProductDeveloperAadGroup> productDeveloperAadGroups) => new(entity.Id,
                                                                                                                      entity.ExperimentType?.Name,
                                                                                                                      $"Repetition: {entity.NumberOfRepetition} " +
                                                                                                                      $" | {entity.TemplateSetting?.WaterHardness} [°dH] " +
                                                                                                                      $" | {entity.TemplateSetting?.WaterLevel} [L] " +
                                                                                                                      $" | {entity.TemplateSetting?.ChlorLevel} [ppm] " +
                                                                                                                      $" | {entity.TemplateSetting?.MixingRatio} [Ca2+:Mg2+]" +
                                                                                                                           (entity.ExperimentTypeId.IsPillingTest()
                                                                                                                    ? $" | {entity.TemplateSetting?.RotationTime} [sec] " +
                                                                                                                      $" | {entity.TemplateSetting?.Drumspeed} [rpm]"
                                                                                                                    : ""),
                                                                                                                      entity.Description,
                                                                                                                      entity.Comment,
                                                                                                                      entity.IsActive,
                                                                                                                      [.. productDeveloperAadGroups.Select(a => a.ToDto())],
                                                                                                                      entity.MonitorSetups?.Select(ms => $"{ms.Monitor?.Name} | {ms.Monitor?.MonitorType?.Name} | {ms.DeviceType?.Name} {(ms.IsUvMeasurement ? "[UV]" : "")}").ToList(),
                                                                                                                      entity.TemplateAdditives?.Select(asu => $"{(asu.AdditiveType.Category.Trim().ToUpper().Equals("DTI", StringComparison.OrdinalIgnoreCase) ? $"{asu.AdditiveType?.Name} [Product Dependent]" : asu.AdditiveType?.Name)} | {asu.Amount} [{asu.AdditiveType?.Unit}]").ToList());
    public static ExperimentSummaryDto ToExperimentSummaryDto(this Experiment entity) => new(entity.Id,
                                                                                             entity.ExperimentStatusId,
                                                                                             entity.ExperimentStatus?.Name,
                                                                                             entity.ExperimentType?.Name,
                                                                                             entity.ExperimentTypeId,
                                                                                             entity.Description,
                                                                                             entity.Comment,
                                                                                             entity.NumberOfRepetition,
                                                                                             entity.RequestedProgram,
                                                                                             entity.RequestedTemperature,
                                                                                             entity.IsExternal,
                                                                                             entity.StatusOpenDate,
                                                                                             entity.StatusValidationDate,
                                                                                             entity.StatusPreparationDate,
                                                                                             entity.StatusMeasurementDate,
                                                                                             entity.StatusCompletedDate,
                                                                                             entity.ProductDeliveryDate,
                                                                                             entity.StartingCalendarWeekDate,
                                                                                             entity.MonitorSetups?.Select(ms => $"{ms.Monitor?.Name} | {ms.Monitor?.MonitorType?.Name} | {ms.DeviceType?.Name} {(ms.IsUvMeasurement ? "[UV]" : "")}").ToList(),
                                                                                            [.. entity.ProductSetups.OrderBy(ps => ps.Position)
                                                                                                                    .Select(ps => new ProdcutSetupSummaryDto(
                                                                                                                        $"{ps.Product?.HugoWorksheetId} | {ps.Product?.Description} | R: {entity.NumberOfRepetition} | WH: {ps.WaterHardness} [°dH] | WL: {ps.WaterLevel} [L] | WM: {ps.WashingMachine?.Name} - {ps.WashingMachine?.Temperature} [°C]"
                                                                                                                        + (entity.ExperimentTypeId.IsPillingTest()
                                                                                                                            ? $" | RT: {ps.RotationTime} [sec] | DS: {ps.Drumspeed} [rpm]"
                                                                                                                            : ""),
                                                                                                                        ps.Position))
]
);

    public static UpdateExperimentDto ToUpdateExperimentDto(this Experiment entity) => new(entity.Id,
                                                                                           entity.ExperimentTypeId,
                                                                                           entity.ExperimentStatusId,
                                                                                           entity.Comment,
                                                                                           entity.Description,
                                                                                           entity.NumberOfRepetition,
                                                                                           entity.IsActive,
                                                                                           entity.IsExternal,
                                                                                           entity.RequestedProgram,
                                                                                           entity.RequestedTemperature,
                                                                                           entity.ProductDeliveryDate,
                                                                                           entity.StartingCalendarWeekDate,
                                                                                           entity.OperatorAadGroupId,
                                                                                           entity.ValidatorAadGroupId,
                                                                                           entity.TemplateSetting?.ToDto());

    public static void Map(this UpdateExperimentDto dto, Experiment entity)
    {
        entity.ExperimentTypeId = dto.ExperimentTypeId;
        entity.ExperimentStatusId = dto.ExperimentStatusId;
        entity.Description = dto.Description;
        entity.Comment = dto.Comment;
        entity.NumberOfRepetition = dto.NumberOfRepetition;
        entity.IsActive = dto.IsActive;
        entity.IsExternal = dto.IsExternal;
        entity.RequestedProgram = dto.RequestedProgram;
        entity.RequestedTemperature = dto.RequestedTemperature.Value;
        entity.OperatorAadGroupId = dto.OperatorAadGroupId;
        entity.ValidatorAadGroupId = dto.ValidatorAadGroupId;
        entity.ProductDeliveryDate = dto.ProductDeliveryDate;
        entity.StartingCalendarWeekDate = dto.StartingCalendarWeekDate;
        if (!Guard.Against.IsNull(entity.TemplateSetting))
            dto.TemplateSetting?.Map(entity.TemplateSetting);
    }

}
