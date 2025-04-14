using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IGymCoachService
    {
        Task<GymCoachDTO?> CreateAsync(GymCoachCreateDTO dto);
        Task<IEnumerable<GymCoachDTO>> GetAllAsync();
        Task<GymCoachDTO?> GetByIdAsync(int id);
        Task<GymCoachDTO?> UpdateAsync(int id, GymCoachCreateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<GymCoachDTO>> GetByGymIdAsync(int gymId);
    }
}
