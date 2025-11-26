using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;

public sealed record UpdateExperimentDto(long Id,
                                         long ExperimentTypeId,
                                         long ExperimentStatusId,
                                         string? Comment,
                                         string Description,
                                         int NumberOfRepetition,
                                         bool IsActive,
                                         bool IsExternal,
                                         string? RequestedProgram,
                                         int? RequestedTemperature,
                                         DateTime? ProductDeliveryDate,
                                         DateTime? StartingCalendarWeekDate,
                                         long? OperatorAadGroupId,
                                         long? ValidatorAadGroupId,
                                         TemplateSettingDto? TemplateSetting);