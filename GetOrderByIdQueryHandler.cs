using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrderById;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrderById;

public sealed class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
{
    public async Task<ErrorOr<GetOrderByIdQueryResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.Orders.FindOneAsync(o => o.Id == request.OrderId,
                                                         o => o.Include(o => o.Products)
                                                         .ThenInclude(p => p.ProductionType)
                                                         .Include(o => o.Products).
                                                         ThenInclude(p => p.ProductType)
                                                         .Include(o => o.ProductDeveloperAadGroup)
                                                         .ThenInclude(o => o.DefaultValidatorAadGroup)
                                                         .Include(o => o.ProductDeveloperAadGroup)
                                                         .ThenInclude(o => o.DefaultOperatorAadGroup),
                                                         cancellationToken);

        if (Guard.Against.IsNull(order))
            return Errors.Infrastructure.DoesNotExist("Order");

        return new GetOrderByIdQueryResponse(order.ToDto());

    }
}
