
using Microsoft.Extensions.Caching.Memory;
using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Application;

public class ProductService : IProductService
{
    private IProductsRepository _repository;
    private readonly IMemoryCache _cache;

    public ProductService(IProductsRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var result = await _repository.GetAllProductAsync();

        return result;
    }

    public async Task<Product> GetAsync(int id)
    {
        string key = $"Product:{id}";
        if (!_cache.TryGetValue(key, out Product? product))
        {
            product = await _repository.GetProductAsync(id);

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1));

            _cache.Set(key, product, options);
        }

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var result = await _repository.UpdateProductAsync(product);

        string key = $"Product:{product.Id}";
        _cache.Remove(key);

        return result;
    }

    public async Task<Product> AddAsync(Product product)
    {
        var result = await _repository.AddProductAsync(product);

        return result;
    }

    public Product Add(Product product)
    {
        var result = _repository.AddProductAsync(product).Result;

        return result;
    }
}
