using System.Linq.Expressions;

namespace TaskManagerSample.Core.Intefaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Core.Models.Entity
{
    Task Add(TEntity entity);

    Task<TEntity> GetById(Guid id);

    Task<List<TEntity>> GetList();

    Task Update(TEntity entity);

    Task Delete(Guid id);

    Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate);

    Task<int> SaveChanges();
}