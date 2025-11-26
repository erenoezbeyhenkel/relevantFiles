namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;

public sealed record ExpandOrderCommandResponse(long OrderId,
                                                string InternalId,
                                                long ExperimentId);