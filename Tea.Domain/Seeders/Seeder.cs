using Microsoft.EntityFrameworkCore;
using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Domain.Seeders;

public class Seeder(DataContext context) : ISeeder
{
    public async Task Seed()
    {
        if (!context.Set<Product>().Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Tea1",
                    Ean = "1111111111111",
                    Price = 10,
                    Stock = 10,
                    Sku = "TEA001",

                },
                new Product
                {
                    Name = "Tea2",
                    Ean = "2222222222222",
                    Price = 20,
                    Stock = 20,
                    Sku = "TEA002",

                },
                new Product
                {
                    Name = "Tea3",
                    Ean = "3333333333333",
                    Price = 30,
                    Stock = 30,
                    Sku = "TEA003",
                }
            };

            context.Set<Product>().AddRange(products);
            context.SaveChanges();
        }
    }
}