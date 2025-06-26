using EShop.MVC.Data.Domain;
using EShop.MVC.Data.Respositories;

namespace EShop.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);

        Task CreateProductAsync(Product product, CancellationToken cancellationToken = default);
    }

    public class CatalogService(IUnitOfWork uow) : ICatalogService
    {
        private readonly IGenericRepository<Product> _productRepository = uow.GetRepository<Product>();

        public async Task CreateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            _productRepository.Add(product);
            await uow.SaveChangesAsync(cancellationToken);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            return _productRepository.GetAllAsync(cancellationToken);
        }
    }
}