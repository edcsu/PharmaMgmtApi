using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;
using PharmaMgmtApi.Models;

namespace PharmaMgmtApi.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}