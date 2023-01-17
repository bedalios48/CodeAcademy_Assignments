using System.Linq.Expressions;

namespace GenealogyTree.Domain.Interfaces.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CRUD
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null
            ,Expression<Func<TEntity, object>>? filterInclude = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<int> CreateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task SaveAsync();
    }
}
