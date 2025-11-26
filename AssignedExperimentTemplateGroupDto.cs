namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record AssignedExperimentTemplateGroupDto(long Id,
                                                        long ExperimentTemplateId,
                                                        long AssignedExperimentProductDeveloperAadGroupId);