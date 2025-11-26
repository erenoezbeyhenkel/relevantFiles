using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Products.Product;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;

public sealed record CreateExperimentDto(long OrderId,
                                         long ExperimentTemplateId,
                                         IEnumerable<ProductDto> Products,
                                         long? ExperimentIdForProductsOrder);