using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ProductSetup;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record ExperimentSummaryDto(long ExperimentId,
                                          long ExperimentStatusId,
                                          string ExperimentStatusName,
                                          string ExperimentType,
                                          long ExperimentTypeId,
                                          string Description,
                                          string Comment,
                                          int NumberOfRepetition,
                                          string RequestedProgram,
                                          int RequestedTemperature,
                                          bool IsExternal,
                                          DateTime StatusOpenDate,
                                          DateTime? StatusValidationDate,
                                          DateTime? StatusPreparationDate,
                                          DateTime? StatusMeasurementDate,
                                          DateTime? StatusCompletedDate,
                                          DateTime? ProductDeliveryDate,
                                          DateTime? StartingCalendarWeekDate,
                                          List<string> MonitorSetups,
                                          List<ProdcutSetupSummaryDto> ProdcutSetupSummaryDtos);