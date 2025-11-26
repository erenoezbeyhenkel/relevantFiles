using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AzureGroups.GetAllProductDeveloperAadGroups;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;

public sealed record ExperimentTemplateSummaryDto(long ExperimentId,
                                                  string ExperimentType,
                                                  string TemplateSettings,
                                                  string Description,
                                                  string Comment,
                                                  bool IsActive,
                                                  List<ProductDeveloperAadGroupDto> AssignedProductDeveloperAadGroups,
                                                  List<string> MonitorSetups,
                                                  List<string> TemplateAdditives);