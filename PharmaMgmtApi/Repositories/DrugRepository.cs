using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;
using PharmaMgmtApi.Models;

namespace PharmaMgmtApi.Repositories;

public class DrugRepository : GenericRepository<Drug>, IDrugRepository
{
    public DrugRepository(AppDbContext dbContext) : base(dbContext)
    {

    }
}