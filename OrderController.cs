using Hcb.Rnd.Pwn.Api.Controllers.Base;
using Hcb.Rnd.Pwn.Common.Constants;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Delete;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Update;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrderById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByCursor;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByExperimentStatus;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersForOperator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersForValidator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.OrdersByHugoProjectId;
using Hcb.Rnd.Pwn.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hcb.Rnd.Pwn.Api.Controllers.Orders;

/// <summary>
/// Includes the order features
/// </summary>
/// <param name="sender"></param>
[Route("api/v1.0/order/")]
public sealed class OrderController(ISender sender) : ApiController(sender)
{
    /// <summary>
    /// To create order.
    /// </summary>
    /// <param name="createOrderCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpPost("create")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(CreateOrderCommandResponse))]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        var createResult = await Sender.Send(createOrderCommand, cancellationToken);
        return createResult.Match(
            createResult => CreatedAtAction(nameof(Create), createOrderCommand, createResult),
            errors => Problem(errors)
            );
    }

    /// <summary>
    /// To expand existing order.
    /// </summary>
    /// <param name="expandOrderCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpPut("expand")]
    [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(ExpandOrderCommandResponse))]
    public async Task<IActionResult> Expand([FromBody] ExpandOrderCommand expandOrderCommand, CancellationToken cancellationToken)
    {
        var expandResult = await Sender.Send(expandOrderCommand, cancellationToken);

        return expandResult.Match(
        expandResult => Ok(expandResult),
        errors => Problem(errors)
        );
    }

    /// <summary>
    /// To update any order.
    /// </summary>
    /// <param name="updateOrderCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloper)]
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateOrderCommandResponse))]
    public async Task<IActionResult> Update([FromBody] UpdateOrderCommand updateOrderCommand, CancellationToken cancellationToken)
    {
        var updateResult = await Sender.Send(updateOrderCommand, cancellationToken);

        return updateResult.Match(
           updateResult => Ok(updateResult),
           errors => Problem(errors)
           );
    }


    /// <summary>
    /// To delete order physically.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteOrderCommandResponse))]
    public async Task<IActionResult> Delete([FromQuery(Name = "orderId")] long orderId, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteOrderCommand(orderId), cancellationToken);

        return result.Match(
           result => Ok(result),
           errors => Problem(errors)
           );
    }

    /// <summary>
    /// To get orders summary depends on the hugo project id.
    /// </summary>
    /// <param name="hugoProjectId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpGet("getOrdersByHugoProjectId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersByHugoProjectIdQueryResponse))]
    public async Task<IActionResult> GetOrdersByHugoProjectId([FromQuery(Name = "hugoProjectId")] string hugoProjectId, CancellationToken cancellationToken)
    {
        var orders = await Sender.Send(new GetOrdersByHugoProjectIdQuery(hugoProjectId), cancellationToken);

        return orders.Match(
           orders => Ok(orders),
           errors => Problem(errors)
           );
    }

    /// <summary>
    /// To get the order details by Id.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndOperator)]
    [HttpGet("getOrderById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderByIdQueryResponse))]
    public async Task<IActionResult> GetOrderById([FromQuery(Name = "orderId")] long orderId, CancellationToken cancellationToken)
    {
        var order = await Sender.Send(new GetOrderByIdQuery(orderId), cancellationToken);

        return order.Match(
           order => Ok(order),
           errors => Problem(errors)
           );
    }

    /// <summary>
    /// To get all orders depends on the filters
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerProductDeveloper)]
    [HttpGet("getOrdersByCursor")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersByCursorQueryResponse))]
    public async Task<IActionResult> GetOrdersByCursor([FromQuery(Name = "cursor")] long cursor, [FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to, CancellationToken cancellationToken)
    {
        var orders = await Sender.Send(new GetOrdersByCursorQuery(cursor, from, to), cancellationToken);

        return orders.Match(
           orders => Ok(orders),
           errors => Problem(errors)
           );
    }

    /// <summary>
    /// To get all orders depends on the filters (especially experiment status)
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="statuses"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidatorAndProductDeveloperAndOperator)]
    [HttpGet("getOrdersByExperimentStatus")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersByExperimentStatusQueryResponse))]
    public async Task<IActionResult> GetOrdersByExperimentStatus([FromQuery(Name = "cursor")] long cursor, [FromQuery(Name = "statuses")] IEnumerable<StatusExperiment> statuses, [FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to, CancellationToken cancellationToken)
    {
        var orders = await Sender.Send(new GetOrdersByExperimentStatusQuery(cursor, statuses, from, to), cancellationToken);

        return orders.Match(
           orders => Ok(orders),
           errors => Problem(errors)
           );
    }

    /// <summary>
    ///  To get all orders depends on the filters (especially experiment status)
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="statuses"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashOperator)]
    [HttpGet("getOrdersForOperator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersForOperatorQueryResponse))]
    public async Task<IActionResult> GetOrdersForOperator([FromQuery(Name = "cursor")] long cursor, [FromQuery(Name = "statuses")] IEnumerable<StatusExperiment> statuses, [FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to, CancellationToken cancellationToken)
    {
        var orders = await Sender.Send(new GetOrdersForOperatorQuery(new GetOrdersForOperatorQueryDto(cursor, statuses, from, to)), cancellationToken);

        return orders.Match(
           orders => Ok(orders),
           errors => Problem(errors)
           );
    }

    /// <summary>
    /// To get all orders depends on the filters (especially experiment status)
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="statuses"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = Roles.WashManagerValidator)]
    [HttpGet("getOrdersForValidator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrdersForValidatorQueryResponse))]
    public async Task<IActionResult> GetOrdersForValidator([FromQuery(Name = "cursor")] long cursor, [FromQuery(Name = "statuses")] IEnumerable<StatusExperiment> statuses, [FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to, CancellationToken cancellationToken)
    {
        var orders = await Sender.Send(new GetOrdersForValidatorQuery(new GetOrdersForValidatorQueryDto(cursor, statuses, from, to)), cancellationToken);

        return orders.Match(
           orders => Ok(orders),
           errors => Problem(errors)
           );
    }
}
