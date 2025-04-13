﻿using Gym_Community.API.DTOs.E_comm;
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

        public async Task<CategoryDTO?> CreateCategory(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                IsDeleted = categoryDto.IsDeleted
            };

            var result = await _categoryRepository.AddAsync(category);

            return result == null ? null : new CategoryDTO
            {
                CategoryID = result.CategoryID,
                Name = result.Name,
                IsDeleted = result.IsDeleted
            };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _categoryRepository.ListAsync();
            return categories.Select(c => new CategoryDTO
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                IsDeleted = c.IsDeleted
            }).ToList();
        }

        public async Task<CategoryDTO?> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            return category == null ? null : new CategoryDTO
            {
                CategoryID = category.CategoryID,
                Name = category.Name,
                IsDeleted = category.IsDeleted
            };
        }

        public async Task<bool> UpdateCategory(int categoryId, CategoryDTO categoryDto)
        {
            var existing = await _categoryRepository.GetById(categoryId);
            if (existing == null) return false;

            existing.Name = categoryDto.Name;
            existing.IsDeleted = categoryDto.IsDeleted;

            var updated = await _categoryRepository.UpdateAsync(existing);
            return updated != null;
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);
            if (category == null) return false;

            return await _categoryRepository.RemoveAsync(category);
        }
    }
}