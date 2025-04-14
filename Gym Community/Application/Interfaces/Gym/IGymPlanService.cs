using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IGymPlanService
    {
        Task<GymPlanReadDTO?> CreateAsync(GymPlanCreateDTO dto);
        Task<IEnumerable<GymPlanReadDTO>> GetAllAsync();
        Task<GymPlanReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<GymPlanReadDTO>> GetByGymIdAsync(int gymId);
        Task<GymPlanReadDTO?> UpdateAsync(int id, GymPlanCreateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
