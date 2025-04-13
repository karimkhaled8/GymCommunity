using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IGymService
    {
        Task<GymReadDTO?> AddAsync(GymCreateDTO gym);
        Task<IEnumerable<GymReadDTO>> ListAsync();
        Task<GymReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<GymReadDTO>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm);
        Task<GymReadDTO?> UpdateAsync(int id, GymCreateDTO gym);
        Task<bool> DeleteAsync(int id);
    }
}
