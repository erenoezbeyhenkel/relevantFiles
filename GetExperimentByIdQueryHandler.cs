using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.MicrosoftGraph;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Dashboard.RecentlyViewedExperiment.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetById;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetById;

public sealed class GetExperimentByIdQueryHandler(IUnitOfWork unitOfWork,
                                                  ISender sender,
                                                  IMicrosoftGraphService graphService) : IQueryHandler<GetExperimentByIdQuery, GetExperimentByIdQueryResponse>
{
    public async Task<ErrorOr<GetExperimentByIdQueryResponse>> Handle(GetExperimentByIdQuery request, CancellationToken cancellationToken)
    {
        var experiment = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.ExperimentId,
            e => e.Include(aad => aad.AssignedExperimentTemplateGroups)
            .ThenInclude(aad => aad.ProductDeveloperAadGroup)
            .ThenInclude(aad => aad.DefaultValidatorAadGroup)
            .Include(aad => aad.AssignedExperimentTemplateGroups)
            .ThenInclude(aad => aad.ProductDeveloperAadGroup)
            .ThenInclude(e => e.DefaultOperatorAadGroup)
            .Include(e => e.Order)
            .ThenInclude(ag => ag.ProductDeveloperAadGroup)
            .Include(e => e.ValidatorAadGroup)
            .Include(e => e.OperatorAadGroup)
            .Include(e => e.ExperimentType)
            .Include(e => e.ExperimentStatus)
            .Include(e => e.MonitorSetups)
            .ThenInclude(ms => ms.WashCycleSetups)
            .Include(e => e.MonitorSetups)
            .ThenInclude(ms => ms.Monitor)
            .ThenInclude(m => m.MonitorType)
            .Include(e => e.MonitorSetups)
            .ThenInclude(ms => ms.DeviceType)
            .Include(e => e.ProductSetups)
            .ThenInclude(ps => ps.Product)
            .Include(e => e.ProductSetups)
            .ThenInclude(ps => ps.Product)
            .ThenInclude(p => p.ProductType)
            .Include(e => e.ProductSetups)
            .ThenInclude(ps => ps.Product)
            .ThenInclude(p => p.ProductionType)
            .Include(e => e.ProductSetups)
            .ThenInclude(ps => ps.Additives)
            .ThenInclude(a => a.AdditiveType)
            .Include(e => e.ProductSetups)
            .ThenInclude(ps => ps.WashingMachine), cancellationToken);

        if (Guard.Against.IsNull(experiment))
            return Errors.Infrastructure.DoesNotExist("Experiment");

        var createdByUserName = await graphService.GetUserNameById(experiment.CreatedById, cancellationToken);
        var updatedByUserName = await graphService.GetUserNameById(experiment.GetExperimentLastUpdatedById(), cancellationToken);

        var experimentDto = experiment.ToDto(createdByUserName, updatedByUserName);
        await sender.Send(new CreateRecentlyViewedExperimentCommand(new(experiment.Id, false)), cancellationToken: cancellationToken);
        return new GetExperimentByIdQueryResponse(experimentDto);
    }
}
