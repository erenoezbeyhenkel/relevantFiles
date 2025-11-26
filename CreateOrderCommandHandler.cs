using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Products;
using Hcb.Rnd.Pwn.Application.Common.Helpers;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Create;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using MediatR;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Create;

public sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork,
                                              ISender sender,
                                              IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    public async Task<ErrorOr<CreateOrderCommandResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = request.Order.ToEntity();

        var internalId = await GeneralHelper.GenerateInternalId(unitOfWork, dateTimeProvider, cancellationToken);
        order.InternalId = internalId;

        order.ProductDeveloperAadGroupId = order.ProductDeveloperAadGroup.Id;
        order.ProductDeveloperAadGroup = null;

        await unitOfWork.Orders.InsertOneAsync(order, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 1)
            return Errors.Infrastructure.CreationError("Order");

        if (request.Order.ExperimentTemplateId > 0)
        {
            var products = order.Products.Where(product => request.Order.Products.Where(p => p.IsSelected).ToList()
            .Any(item => product.HugoWorksheetId == item.HugoWorksheetId &&
                         product.HugoProductId == item.HugoProductId &&
                         product.HugoBatchId == item.HugoBatchId &&
                         product.HugoSampleId == item.HugoSampleId));

            var createExperimentResult = await sender.Send(new CreateExperimentCommand(new CreateExperimentDto(order.Id,
                                                                                                               request.Order.ExperimentTemplateId,
                                                                                                               products.Select(p => p.ToDto()),
                                                                                                               default)), cancellationToken);
            if (createExperimentResult.IsError)
                return createExperimentResult.FirstError;

            var createdExperiment = createExperimentResult.Value.Experiment;
            if (Guard.Against.IsNull(createdExperiment))
                return Errors.Infrastructure.CreationError("Experiment");

            return new CreateOrderCommandResponse(order.Id, internalId, createdExperiment.Id);
        }

        return new CreateOrderCommandResponse(order.Id, internalId, default);
    }
}
