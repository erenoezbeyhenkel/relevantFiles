using ErrorOr;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentsByProductDeveloperGroup;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetExperimentsByProductDeveloperGroup;

public sealed class GetExperimentsCountByProductDeveloperGroupQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetExperimentsCountByProductDeveloperGroupQuery, GetExperimentsCountByProductDeveloperGroupQueryResponse>
{
    public async Task<ErrorOr<GetExperimentsCountByProductDeveloperGroupQueryResponse>> Handle(GetExperimentsCountByProductDeveloperGroupQuery request, CancellationToken cancellationToken)
    {
        var experiments = await unitOfWork.Experiments.FilterByEnumerableAsync(e => e.Order.ProductDeveloperAadGroup.Oid.Equals(request.ProductDeveloperGroupId)
                                                                              && e.StatusOpenDate.AddDays(request.NumberOfDays) >= DateTime.Now,
                                                                              e=>e.Include(aad=>aad.ExperimentStatus),
                                                                              cancellationToken: cancellationToken);

        return new GetExperimentsCountByProductDeveloperGroupQueryResponse(experiments.GroupBy(e => e.ExperimentStatus.Name)
            .Select(x => new ExperimentCountByProductDeveloperGroupDto(x.Key, x.Count()))
            .ToList());
    }
}
