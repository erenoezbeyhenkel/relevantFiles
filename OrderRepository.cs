using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Orders;
using Hcb.Rnd.Pwn.Domain.Entities.Orders;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Orders;

public sealed class OrderRepository(DataBaseContext dataBaseContext) : GenericRepository<Order>(dataBaseContext), IOrderRepository
{
}
