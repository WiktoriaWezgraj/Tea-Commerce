
using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Application;

public class ProductService : IProductService
{
    private IProductsRepository _repository;

    public ProductService(IProductsRepository repository)
    {
        _repository = repository;
    }


    public async Task<List<Product>> GetAllAsync()
    {
        var result = await _repository.GetAllProductAsync();
        return result;
    }

    public async Task<Product> GetAsync(int id)
    {
        var result = await _repository.GetProductAsync(id);
        return result;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var result = await _repository.UpdateProductAsync(product);
        return result;
    }

    public async Task<Product> AddAsync(Product product)
    {
        var result = await _repository.AddProductAsync(product);
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.DeleteProductAsync(id);
        return result;
    }
}