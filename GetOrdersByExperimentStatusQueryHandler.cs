using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByExperimentStatus;
using Hcb.Rnd.Pwn.Common.Enums;
using Hcb.Rnd.Pwn.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrdersByExperimentStatus;

public sealed class GetOrdersByExperimentStatusQueryHandler(IUnitOfWork unitOfWork,
                                                            IPwnHttpContextAccessor pwnHttpContextAccessor,
                                                            IOptions<OrderManagementOptions> options) : IQueryHandler<GetOrdersByExperimentStatusQuery, GetOrdersByExperimentStatusQueryResponse>
{
    public async Task<ErrorOr<GetOrdersByExperimentStatusQueryResponse>> Handle(GetOrdersByExperimentStatusQuery request, CancellationToken cancellationToken)
    {
        //Find the experiments depends on the filters
        var experiments = await unitOfWork.Experiments.FilterByEnumerableAsync(e =>
           e.CreatedAt >= request.From
        && e.CreatedAt <= request.To
        && request.ExperimentStatus.Contains((StatusExperiment)e.ExperimentStatus.Id)
        && pwnHttpContextAccessor.UserGroups.Contains(e.Order.ProductDeveloperAadGroup.Oid.ToString()), cancellationToken: cancellationToken);

        //Get the list of relevant experiments
        var experimentIds = experiments.Select(e => e.Id);
        if (!Guard.Against.IsAnyOrNotEmpty(experimentIds))
            return new GetOrdersByExperimentStatusQueryResponse([], 0, 0, request.Cursor);

        //Fetch the orders of the filtered experimens
        var ordersQuery = unitOfWork.Orders.Query
         .Where(order => order.Experiments.Any(ex => experimentIds.Contains(ex.Id)))
         .Include(order => order.ProductDeveloperAadGroup)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.ExperimentType)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.ExperimentStatus)
         .Include(order => order.ProductDeveloperAadGroup)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.MonitorSetups)
         .ThenInclude(ms => ms.Monitor)
         .ThenInclude(m => m.MonitorType)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.MonitorSetups)
         .ThenInclude(ms => ms.DeviceType)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.ProductSetups)
         .ThenInclude(ps => ps.WashingMachine)
         .Include(order => order.Experiments)
         .ThenInclude(experiment => experiment.ProductSetups)
         .ThenInclude(ps => ps.Product)
         .AsQueryable().AsNoTracking();

        var pageSize = options.Value.PageSize;
        var (orders, nextCursor) = await unitOfWork.Orders.CursorPaginationDescendingAsync(ordersQuery, pageSize, request.Cursor, cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(orders))
            return new GetOrdersByExperimentStatusQueryResponse([], 0, 0, request.Cursor);

        var orderDtos = orders.Select(ao => ao.ToDto(experimentIds)).ToList();
        var orderCount = await unitOfWork.Orders.CursorPaginationTotalItemCountAsync(ordersQuery, request.From, request.To, cancellationToken);
        return new GetOrdersByExperimentStatusQueryResponse(orderDtos, orderCount, (int)Math.Ceiling((decimal)orderCount / pageSize), nextCursor);
    }
}
