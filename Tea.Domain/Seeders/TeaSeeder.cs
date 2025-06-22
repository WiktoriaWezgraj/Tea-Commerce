using Microsoft.EntityFrameworkCore;
using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Domain.Seeders
{
    public class TeaSeeder : ITeaSeeder
    {
        private readonly DataContext _context;

        public TeaSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            // Seeder produktów
            if (!await _context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Tea1", Ean = "1111111111111", Price = 10, Stock = 10, Sku = "TEA001" },
                    new Product { Name = "Tea2", Ean = "2222222222222", Price = 20, Stock = 20, Sku = "TEA002" },
                    new Product { Name = "Tea3", Ean = "3333333333333", Price = 30, Stock = 30, Sku = "TEA003" }
                };

                _context.Products.AddRange(products);
                await _context.SaveChangesAsync();
            }

            // Seeder kategorii
            if (!await _context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Tea" },
                    new Category { Name = "Herbs" },
                    new Category { Name = "Accessories" }
                };

                _context.Categories.AddRange(categories);
                await _context.SaveChangesAsync();
            }
        }
    }
}
