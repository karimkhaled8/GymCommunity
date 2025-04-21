using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces
{
    public interface IGymRepository
    {
       public Task<Gym_Community.Domain.Models.Gym.Gym?> AddAsync(Gym_Community.Domain.Models.Gym.Gym gym);
       public Task<IEnumerable<Gym_Community.Domain.Models.Gym.Gym>> ListAsync();
       public Task<Gym_Community.Domain.Models.Gym.Gym?> GetByIdAsync(int id);
       public Task<IEnumerable<Domain.Models.Gym.Gym>> GetByOwnerIdAsync(string ownerId);

       public Task<IEnumerable<Gym_Community.Domain.Models.Gym.Gym>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm);
       public Task<Gym_Community.Domain.Models.Gym.Gym?> UpdateAsync(Gym_Community.Domain.Models.Gym.Gym gym);
       public Task<bool> DeleteAsync(Gym_Community.Domain.Models.Gym.Gym gym);
    }
}
