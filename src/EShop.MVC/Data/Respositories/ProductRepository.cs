using EShop.MVC.Data.Domain;

namespace EShop.MVC.Data.Respositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // Additional methods specific to ProductRepository can be defined here
    }

    public class ProductRepository(ApplicationDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        // Additional methods specific to ProductRepository can be added here
    }
}