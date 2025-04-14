using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IGymImgService
    {
        Task<GymImgReadDTO?> CreateAsync(GymImgCreateDTO dto);
        Task<IEnumerable<GymImgReadDTO>> GetAllAsync();
        Task<GymImgReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<GymImgReadDTO>> GetByGymIdAsync(int gymId);
        Task<GymImgReadDTO?> UpdateAsync(int id, GymImgCreateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
