using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class AssignedExperimentTemplateGroupMapperExtension
{
    public static AssignedExperimentTemplateGroup ToEntity(this AssignedExperimentTemplateGroupDto dto) => new()
    {
        Id = dto.Id,
        ExperimentId = dto.ExperimentTemplateId,
        AssignedExperimentProductDeveloperAadGroupId = dto.AssignedExperimentProductDeveloperAadGroupId
    };

    public static AssignedExperimentTemplateGroupDto ToDto(this AssignedExperimentTemplateGroup entity) => new(entity.Id,
                                                                                                               entity.ExperimentId,
                                                                                                               entity.AssignedExperimentProductDeveloperAadGroupId);
}
