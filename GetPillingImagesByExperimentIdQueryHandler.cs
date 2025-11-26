using ErrorOr;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetPillingImagesById;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp.GetPillingImagesByWashCycleSetup;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.PillingMetric;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetPillingImagesById;

public sealed class GetPillingImagesByExperimentIdQueryHandler(IUnitOfWork unitOfWork,
                                                               ISender sender) : IQueryHandler<GetPillingImagesByExperimentIdQuery, GetPillingImagesByExperimentIdQueryResponse>
{
    public async Task<ErrorOr<GetPillingImagesByExperimentIdQueryResponse>> Handle(GetPillingImagesByExperimentIdQuery request, CancellationToken cancellationToken)
    {
        var experiment = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.ExperimentId, e => e.Include(e => e.MonitorSetups).ThenInclude(ms => ms.WashCycleSetups), cancellationToken);

        if (Guard.Against.IsNull(experiment))
            return Errors.Infrastructure.DoesNotExist("Experiment");

        var washCycleSetups = experiment.MonitorSetups.SelectMany(ms => ms.WashCycleSetups);

        List<PillingMetricDto> pillingMetricDtos = [];
        foreach (var washCycleSetup in washCycleSetups ?? [])
        {
            var getPillingImagesByWashCycleSetupQueryResult = await sender.Send(new GetPillingImagesByWashCycleSetupQuery(washCycleSetup.Id), cancellationToken);
            if (getPillingImagesByWashCycleSetupQueryResult.IsError)
                return getPillingImagesByWashCycleSetupQueryResult.FirstError;

            pillingMetricDtos.AddRange(getPillingImagesByWashCycleSetupQueryResult.Value.PillingMetricDtos);
        }

        return new GetPillingImagesByExperimentIdQueryResponse(pillingMetricDtos);
    }
}
