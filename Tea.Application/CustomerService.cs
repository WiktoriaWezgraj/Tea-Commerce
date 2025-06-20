using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomersRepository _repository;

        public CustomerService(ICustomersRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _repository.GetAllCustomersAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            return await _repository.GetCustomerAsync(id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            return await _repository.AddCustomerAsync(customer);
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            return await _repository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteCustomerAsync(id);
        }
    }
}

