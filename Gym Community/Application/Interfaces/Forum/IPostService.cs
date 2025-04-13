using Gym_Community.API.DTOs.Forum;
using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Application.Interfaces.Forum
{
    public interface IPostService
    {
        Task<PostReadDTO?> CreateAsync(PostCreateDTO dto);
        Task<IEnumerable<PostReadDTO>> GetAllAsync();
        Task<PostReadDTO?> GetByIdAsync(int id);
        Task<PostReadDTO?> UpdateAsync(int id, PostCreateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PostReadDTO>> GetByUserIdAsync(string userId);
        Task<IEnumerable<PostReadDTO>> GetBySubIdAsync(int subId);
    }
}
