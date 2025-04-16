using Gym_Community.Domain.Models.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface ICoachRatingRepository
    {
        Task<IEnumerable<CoachRating>> GetByCoachIdAsync(string coachId);
        Task<CoachRating?> GetByIdAsync(int id);
        Task AddAsync(CoachRating rating);
        void Delete(CoachRating rating);
        Task<bool> SaveChangesAsync();
    }
}
