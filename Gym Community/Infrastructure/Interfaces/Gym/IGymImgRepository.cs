using Gym_Community.Domain.Models.Gym;

namespace Gym_Community.Infrastructure.Interfaces.Gym
{
    public interface IGymImgRepository
    {
        Task<GymImgs?> AddAsync(GymImgs img);
        Task<IEnumerable<GymImgs>> ListAsync();
        Task<GymImgs?> GetByIdAsync(int id);
        Task<IEnumerable<GymImgs>> GetByGymIdAsync(int gymId);
        Task<GymImgs?> UpdateAsync(GymImgs img);
        Task<bool> DeleteAsync(GymImgs img);
    }
}
