using Microsoft.EntityFrameworkCore;
using Tea.Domain.Models;

namespace Tea.Domain.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly DataContext _dataContext;

    public ProductsRepository(DataContext datacontext)

    {
        _dataContext = datacontext;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _dataContext.Products.Add(product);
        await _dataContext.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await _dataContext.Products.ToListAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _dataContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Product> UpdateProductAsync(Product product)
    {
        _dataContext.Products.Update(product);
        await _dataContext.SaveChangesAsync();
        return product;
    }

    //bool, by wiedzieć, czy usunięcie się udało
    //nie rzucamy bledu gdy nie ma produktu- alternatywa
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _dataContext.Products.FindAsync(id);
        if (product == null)
            return false;

        _dataContext.Products.Remove(product);
        await _dataContext.SaveChangesAsync();
        return true;
    }


}