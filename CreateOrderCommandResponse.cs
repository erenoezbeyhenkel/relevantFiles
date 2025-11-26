namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Create;

public sealed record CreateOrderCommandResponse(long OrderId,
                                                string InternalId,
                                                long ExperimentId);