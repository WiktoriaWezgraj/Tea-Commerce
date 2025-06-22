using Tea.Domain.Models;
using Tea.Domain.Repositories;

namespace Tea.Application
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _repository.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

