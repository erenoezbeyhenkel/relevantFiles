using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AzureGroups.GetAllOperatorAadGroups;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AzureGroups.GetAllValidatorAadGroups;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ExperimentStatus;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ExperimentType;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.MonitorSetUp;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ProductSetup;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.TemplateAdditive;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record ExperimentDto(long Id,
                                   long? OrderId,
                                   long ExperimentTypeId,
                                   string AssignedGroupName,
                                   string InternalId,
                                   DateTime? OrderCreatedAt,
                                   ExperimentTypeDto ExperimentType,
                                   long ExperimentStatusId,
                                   ExperimentStatusDto ExperimentStatus,
                                   DateTime StatusOpenDate,
                                   DateTime? StatusValidationDate,
                                   DateTime? StatusPreparationDate,
                                   DateTime? StatusMeasurementDate,
                                   DateTime? StatusCompletedDate,
                                   DateTime? ProductDeliveryDate,
                                   DateTime? StartingCalendarWeekDate,
                                   string Description,
                                   string Comment,
                                   int NumberOfRepetition,
                                   bool IsActive,
                                   bool IsExternal,
                                   long? TemplateExperimentId,
                                   long TemplateSettingId,
                                   TemplateSettingDto TemplateSetting,
                                   long? ValidatorAadGroupId,
                                   ValidatorAadGroupDto ValidatorAadGroupDto,
                                   long? OperatorAadGroupId,
                                   OperatorAadGroupDto OperatorAadGroupDto,
                                   string RequestedProgram,
                                   int RequestedTemperature,
                                   ICollection<MonitorSetupDto> MonitorSetups,
                                   ICollection<ProductSetupDto> ProductSetups,
                                   ICollection<TemplateAdditiveDto> TemplateAdditives,
                                   List<AssignedExperimentTemplateGroupDto> AssignedExperimentTemplateGroupDtos,
                                   string CreatedBy,
                                   string UpdatedBy);


