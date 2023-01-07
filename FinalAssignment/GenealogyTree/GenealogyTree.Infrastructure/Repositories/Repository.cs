using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GenealogyTree.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GenealogyTreeContext _db;
        private DbSet<TEntity> _dbSet;

        public Repository(GenealogyTreeContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter == null)
            {
                throw new NotImplementedException();
            }

            return await query.AnyAsync(filter);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
