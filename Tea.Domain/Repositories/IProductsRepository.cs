using Tea.Domain.Models;

namespace Tea.Domain.Repositories;

public interface IProductsRepository
{

    Task<Product> GetProductAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<List<Product>> GetAllProductAsync();
    Task<bool> DeleteProductAsync(int id);
}