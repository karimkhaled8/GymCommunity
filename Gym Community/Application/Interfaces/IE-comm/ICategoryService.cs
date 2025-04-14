using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface ICategoryService
    {
        Task<CategoryDTO?> CreateCategory(CategoryDTO categoryDto);
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO?> GetCategoryById(int categoryId);
        Task<CategoryDTO?> UpdateCategory(int categoryId, CategoryDTO categoryDto);
        Task<bool> DeleteCategory(int categoryId);
    }
}
