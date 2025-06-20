using Tea.Domain.Models;

namespace Tea.Domain.Repositories
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}

