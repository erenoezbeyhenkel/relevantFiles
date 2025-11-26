using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByCursor;
using Hcb.Rnd.Pwn.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrdersByCursor;

public sealed class GetOrdersByCursorQueryHandler(IUnitOfWork unitOfWork,
                                                  IPwnHttpContextAccessor pwnHttpContextAccessor,
                                                  IOptions<OrderManagementOptions> options) : IQueryHandler<GetOrdersByCursorQuery, GetOrdersByCursorQueryResponse>
{
    public async Task<ErrorOr<GetOrdersByCursorQueryResponse>> Handle(GetOrdersByCursorQuery request, CancellationToken cancellationToken)
    {
        var ordersQuery = unitOfWork.Orders.Query
          .Where(ao => pwnHttpContextAccessor.UserGroups.Contains(ao.ProductDeveloperAadGroup.Oid.ToString()))
          .Include(o => o.ProductDeveloperAadGroup)
          .ThenInclude(o => o.DefaultValidatorAadGroup)
          .Include(o => o.ProductDeveloperAadGroup)
          .ThenInclude(o => o.DefaultOperatorAadGroup)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.ExperimentType)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.ExperimentStatus)
          .Include(o => o.ProductDeveloperAadGroup)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.MonitorSetups)
          .ThenInclude(ms => ms.Monitor)
          .ThenInclude(m => m.MonitorType)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.MonitorSetups)
          .ThenInclude(ms => ms.DeviceType)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.ProductSetups)
          .ThenInclude(ps => ps.WashingMachine)
          .Include(o => o.Experiments)
          .ThenInclude(e => e.ProductSetups)
          .ThenInclude(ps => ps.Product)
          .AsQueryable()
          .AsTracking();


        var pageSize = options.Value.PageSize;
        var (orders, nextCursor) = await unitOfWork.Orders.CursorPaginationDescendingAsync(ordersQuery, pageSize, request.Cursor, request.From, request.To, cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(orders))
            return new GetOrdersByCursorQueryResponse([], 0, 0, request.Cursor);

        var orderDtos = orders.Select(ao => ao.ToDto()).ToList();

        var ordersCountDependsOnTheFilters = await unitOfWork.Orders.CountAsync(o => o.CreatedAt >= request.From && o.CreatedAt <= request.To && pwnHttpContextAccessor.UserGroups.Contains(o.ProductDeveloperAadGroup.Oid.ToString()),
                                                                                cancellationToken: cancellationToken);

        return new GetOrdersByCursorQueryResponse(orderDtos, ordersCountDependsOnTheFilters, (int)Math.Ceiling((decimal)ordersCountDependsOnTheFilters / pageSize), nextCursor);
    }
}
