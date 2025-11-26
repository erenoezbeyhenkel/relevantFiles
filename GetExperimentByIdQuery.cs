using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Base;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetById;

public sealed record GetExperimentByIdQuery(long ExperimentId) : BaseQuery<GetExperimentByIdQueryResponse>;