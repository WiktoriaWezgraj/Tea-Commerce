using Microsoft.EntityFrameworkCore;
using Tea.Domain.Models;

namespace Tea.Domain.Repositories;

public class Repository : IRepository
{
    private readonly DataContext _datacontext;

    public Repository(DataContext datacontext)

    {
        _datacontext = datacontext;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _datacontext.Products.Add(product);
        await _datacontext.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _datacontext.Products.ToListAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _datacontext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Product> UpdateProductAsync(Product product)
    {
        _datacontext.Products.Update(product);
        await _datacontext.SaveChangesAsync();
        return product;
    }
}