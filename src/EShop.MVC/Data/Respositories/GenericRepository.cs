using EShop.MVC.Data.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace EShop.MVC.Data.Respositories
{
    public interface IGenericRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid id);
    }

    public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext dbContext = dbContext;
        protected readonly DbSet<TEntity> dbSet = dbContext.Set<TEntity>();

        public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return dbSet.FindAsync(id, cancellationToken).AsTask();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(Guid id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    }
}