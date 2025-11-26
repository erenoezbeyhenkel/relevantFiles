using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.StorageAccount;
using Hcb.Rnd.Pwn.Domain.Entities.StorageAccount;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.StorageAccount;

public sealed class StorageAccountTokenRepository(DataBaseContext dataBaseContext) : GenericRepository<StorageAccountToken>(dataBaseContext), IStorageAccountTokenRepository
{
}
