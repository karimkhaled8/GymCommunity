using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO?> CreateCategory(string categoryName)
        {
            var category = new Category
            {
                Name = categoryName,
            };

            var result = await _categoryRepository.AddAsync(category);

            return result == null ? null : new CategoryDTO
            {
                CategoryID = result.CategoryID,
                Name = result.Name,
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _categoryRepository.ListAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
            }).ToList();
        }

        public async Task<CategoryDTO?> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            return category == null ? null : new CategoryDTO
            {
                CategoryID = category.CategoryID,
                Name = category.Name,
            };
        }

        public async Task<CategoryDTO?> UpdateCategory(int categoryId, CategoryDTO categoryDto)
        {
            var existing = await _categoryRepository.GetById(categoryId);
            if (existing == null) return null;

            existing.Name = categoryDto.Name;

            var updated = await _categoryRepository.UpdateAsync(existing);

            return (updated == null) ? null : new CategoryDTO {
                CategoryID = updated.CategoryID,
                Name = updated.Name,
            };
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            if (category == null) return false;

            return await _categoryRepository.RemoveAsync(category);
        }
    }
}