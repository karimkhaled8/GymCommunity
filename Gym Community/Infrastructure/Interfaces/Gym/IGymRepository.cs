using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces
{
    public interface IGymRepository
    {
        Task<Gym?> AddAsync(Gym gym);
        Task<IEnumerable<Gym>> ListAsync();
        Task<Gym?> GetByIdAsync(int id);
        Task<IEnumerable<Gym>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm);
        Task<Gym?> UpdateAsync(Gym gym);
        Task<bool> DeleteAsync(Gym gym);
    }
}
