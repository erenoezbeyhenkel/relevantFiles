using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Update;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Update;

public sealed class UpdateOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResponse>
{
    public async Task<ErrorOr<UpdateOrderCommandResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await unitOfWork.Orders.FindOneAsync(o => o.Id == request.Order.Id,
                                                                 o => o.Include(o => o.Products),
                                                                 cancellationToken: cancellationToken);

        if (Guard.Against.IsNull(existingOrder))
            return Errors.Infrastructure.DoesNotExist("Order");

        request.Order.Map(existingOrder);

        await unitOfWork.Orders.UpdateOneAsync(existingOrder, cancellationToken);
        var updateResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (updateResult <= 0)
            return Errors.Infrastructure.UpdateError("Order");

        return new UpdateOrderCommandResponse(request.Order);
    }
}
