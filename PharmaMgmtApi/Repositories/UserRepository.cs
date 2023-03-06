using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;
using PharmaMgmtApi.Models;

namespace PharmaMgmtApi.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}