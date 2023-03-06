using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PharmaMgmtApi.DbContexts;
using PharmaMgmtApi.Interfaces.IRepositories;

namespace PharmaMgmtApi.Repositories;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
{
    private readonly DbSet<TSource> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbSet = dbContext.Set<TSource>();
    }

    public async Task<TSource> CreateAsync(TSource source)
    {
        var entry = await _dbSet.AddAsync(source);

        return entry.Entity;
    }

    public Task DeleteAsync(TSource source)
        => Task.FromResult(_dbSet.Remove(source));

    public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null, bool isTracking = true)
    {
        var sources = expression is null ? _dbSet : _dbSet.Where(expression);

        return isTracking ? sources : sources.AsNoTracking();
    }

    public async Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression)
        => await _dbSet.FirstOrDefaultAsync(expression);

    public Task<TSource> UpdateAsync(TSource source)
        => Task.FromResult(_dbSet.Update(source).Entity);
}