using Gym_Community.Domain.Models.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface ICoachPortfolioRepository
    {
        Task<IEnumerable<CoachPortfolio>> GetAllAsync();
        Task<int> getporotofolioidasync(string coachId);
        Task<CoachPortfolio?> GetByIdAsync(int id);
        Task<CoachPortfolio?> GetByCoachIdAsync(string coachId);
        Task AddAsync(CoachPortfolio portfolio);
        void Update(CoachPortfolio portfolio);
        void Delete(CoachPortfolio portfolio);
        Task<bool> SaveChangesAsync();
    }
}
