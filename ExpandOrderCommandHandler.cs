using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Products;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Update;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Products.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Products.Product.GetAllByOrder;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using MediatR;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Expand;

public sealed class ExpandOrderCommandHandler(ISender sender) : ICommandHandler<ExpandOrderCommand, ExpandOrderCommandResponse>
{
    public async Task<ErrorOr<ExpandOrderCommandResponse>> Handle(ExpandOrderCommand request, CancellationToken cancellationToken)
    {
        var updateOrderResult = await sender.Send(new UpdateOrderCommand(request.ExpandOrderDto.Order), cancellationToken);
        if (updateOrderResult.IsError)
            return updateOrderResult.FirstError;

        var order = request.ExpandOrderDto.Order.ToEntity();

        //Create new products if any
        var newProducts = request.ExpandOrderDto.Order.Products.Where(p => p.Id == 0);
        if (Guard.Against.IsAnyOrNotEmpty(newProducts))
        {
            var createNewProdutsResult = await sender.Send(new CreateProductsCommand([.. newProducts]), cancellationToken);
            if (createNewProdutsResult.IsError)
                return createNewProdutsResult.FirstError;

            order.Products.ToList().AddRange(createNewProdutsResult.Value.Products.Select(p => p.ToEntity()));
        }

        if (request.ExpandOrderDto.Order.ExperimentTemplateId > 0)
        {
            var getAllProductsByOrderQueryResult = await sender.Send(new GetAllProductsByOrderQuery(request.ExpandOrderDto.Order.Id), cancellationToken);
            if (getAllProductsByOrderQueryResult.IsError)
                return getAllProductsByOrderQueryResult.FirstError;

            //Create experiment base on the selected template
            var products = getAllProductsByOrderQueryResult.Value.Products.Where(product => request.ExpandOrderDto.Order.Products.Where(p => p.IsSelected).ToList()
            .Any(item => product.HugoWorksheetId == item.HugoWorksheetId &&
                         product.HugoProductId == item.HugoProductId &&
                         product.HugoBatchId == item.HugoBatchId &&
                         product.HugoSampleId == item.HugoSampleId));

            var createExperimentResult = await sender.Send(new CreateExperimentCommand(new CreateExperimentDto(order.Id,
                                                                                                               request.ExpandOrderDto.Order.ExperimentTemplateId,
                                                                                                               products,
                                                                                                               request.ExpandOrderDto.ExperimentIdForProductsOrder)), cancellationToken);
            if (createExperimentResult.IsError)
                return createExperimentResult.FirstError;

            var createdExperiment = createExperimentResult.Value.Experiment;
            if (Guard.Against.IsNull(createdExperiment))
                return Errors.Infrastructure.CreationError("Experiment");

            return new ExpandOrderCommandResponse(order.Id, order.InternalId, createdExperiment.Id);
        }

        return new ExpandOrderCommandResponse(order.Id, order.InternalId, default);

    }
}
