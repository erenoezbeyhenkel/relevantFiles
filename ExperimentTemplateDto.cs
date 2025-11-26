namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record ExperimentTemplateDto(long Id,
                                           long ExperimentTypeId,
                                           int NumberOfRepetition,
                                           string Description,
                                           bool IsActive,
                                           string CreatedBy,
                                           string UpdatedBy,
                                           TemplateSettingDto TemplateSetting,
                                           List<AssignedExperimentTemplateGroupDto> AssignedExperimentTemplateGroupDtos);