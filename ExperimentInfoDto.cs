namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record ExperimentInfoDto(long ExperimentId,
                                        long ExperimentStatusId,
                                        long ExperimentTypeId,
                                        string Description,
                                        DateTime StatusOpenDate,
                                        DateTime? StatusPreparationDate,
                                        DateTime? StatusCompleteDate,
                                        ExperimentSummaryDto ExperimentSummaryDto);