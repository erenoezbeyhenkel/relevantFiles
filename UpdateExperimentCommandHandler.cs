using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.Update;

public sealed class UpdateExperimentCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateExperimentCommand, UpdateExperimentCommandResponse>
{
    public async Task<ErrorOr<UpdateExperimentCommandResponse>> Handle(UpdateExperimentCommand request, CancellationToken cancellationToken)
    {
        var existingExperiment = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.UpdateExperimentDto.Id, e => e.Include(e => e.TemplateSetting), cancellationToken);

        if (Guard.Against.IsNull(existingExperiment))
            return Errors.Infrastructure.DoesNotExist("Experiment");

        request.UpdateExperimentDto.Map(existingExperiment);

        await unitOfWork.Experiments.UpdateOneAsync(existingExperiment, cancellationToken);
        var updateResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (updateResult < 1)
            return Errors.Infrastructure.UpdateError("Experiment");

        return new UpdateExperimentCommandResponse(existingExperiment.ToUpdateExperimentDto());
    }
}
