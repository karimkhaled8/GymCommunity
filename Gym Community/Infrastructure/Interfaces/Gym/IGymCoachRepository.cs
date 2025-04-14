using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces.Gym
{
    public interface IGymCoachRepository
    {
       public Task<IEnumerable<GymCoach>> GetByGymId(int gymId);
       public Task<GymCoach?> AddAsync(GymCoach gymCoach);
       public Task<IEnumerable<GymCoach>> ListAsync();
       public Task<GymCoach?> GetByIdAsync(int id);
       public Task<GymCoach?> UpdateAsync(GymCoach gymCoach);
       public Task<bool> DeleteAsync(GymCoach gymCoach);
    }
}
