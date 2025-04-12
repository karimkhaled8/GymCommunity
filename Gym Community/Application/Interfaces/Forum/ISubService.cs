using Gym_Community.API.DTOs.Forum;

namespace Gym_Community.Application.Interfaces.Forum
{
    public interface ISubService
    {
        Task<SubReadDTO?> CreateAsync(SubCreateDTO dto);
        Task<IEnumerable<SubReadDTO>> GetAllAsync();
        Task<SubReadDTO?> GetByIdAsync(int id);
        Task<SubReadDTO?> UpdateAsync(int id, SubCreateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
