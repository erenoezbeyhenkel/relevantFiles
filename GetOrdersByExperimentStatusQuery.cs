using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Base;
using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByExperimentStatus;

public sealed record GetOrdersByExperimentStatusQuery(long Cursor,
                                            IEnumerable<StatusExperiment> ExperimentStatus,
                                            DateTime From,
                                            DateTime To) : BaseQuery<GetOrdersByExperimentStatusQueryResponse>;