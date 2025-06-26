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

        void Delete(TEntity entity); // Delete by entity

        void Delete(Guid id); // Delete by ID

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default); // Async version
    }

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

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

        // Delete by entity (no database hit needed)
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        // Delete by ID (synchronous - might hit database)
        public void Delete(Guid id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        // Async delete by ID
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await dbSet.FindAsync(id, cancellationToken);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    }
}