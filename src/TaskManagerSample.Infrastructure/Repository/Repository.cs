using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Infrastructure.Context;

namespace TaskManagerSample.Infrastructure.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Core.Models.Entity, new()
{
    protected readonly TaskManagerDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(TaskManagerDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetList()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task Add(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public virtual async Task Update(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task Delete(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db?.Dispose();
    }
}