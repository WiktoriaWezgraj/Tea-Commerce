using Tea.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tea.Application
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetAsync(int id);
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
    }
}

