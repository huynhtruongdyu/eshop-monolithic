using EShop.MVC.Data.Domain.Base;
using EShop.MVC.Data.Respositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EShop.MVC.Data
{
    public interface IUnitOfWork
    {
        #region Define methods for committing transactions, rolling back, etc.

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task RollbackAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Define methods for committing transactions, rolling back, etc.

        #region Define repositories

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        //IProductRepository Products { get; }

        #endregion Define repositories
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> repositories = [];

        //private Lazy<IProductRepository> _productRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //_productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_dbContext));
        }

        #region Repositories

        //public IProductRepository Products => _productRepository.Value;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction ??= await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _transaction!.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion Repositories

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }

        IGenericRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<TEntity>(_dbContext);
                repositories[type] = repoInstance;
            }

            return (IGenericRepository<TEntity>)repositories[type];
        }
    }
}