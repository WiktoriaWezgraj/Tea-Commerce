using Microsoft.EntityFrameworkCore;
using Tea.Domain.Models;
using Tea.Domain.Repositories;
using System.Threading.Tasks;

namespace Tea.Domain.Seeders
{
    public class CustomerSeeder : ICustomerSeeder
    {
        private readonly DataContext _context;

        public CustomerSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Customers.AnyAsync())
                return; // check if there's already data in the Customers table

            var customers = new List<Customer>
            {
                new Customer { FirstName = "David", LastName = "Smith", Email = "david.smith@gmail.com", PhoneNumber = "1234567890" },
                new Customer { FirstName = "John", LastName = "Pork", Email = "john.pork@gmail.com", PhoneNumber = "0987654321" },
                new Customer { FirstName = "Mary", LastName = "Johnson", Email = "mary.johnson@gmail.com", PhoneNumber = "1112233445" }
            };

            // Add new customers to the DbSet
            await _context.Customers.AddRangeAsync(customers);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}

