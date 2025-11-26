using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Experiments;

public sealed class ExperimentStatusRepository(DataBaseContext dataBaseContext) : GenericRepository<ExperimentStatus>(dataBaseContext), IExperimentStatusRepository
{
}
