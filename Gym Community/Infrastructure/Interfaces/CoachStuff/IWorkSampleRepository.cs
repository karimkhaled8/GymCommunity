using Gym_Community.Domain.Models.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface IWorkSampleRepository
    {
        Task<IEnumerable<WorkSample>> GetByPortfolioIdAsync(int portfolioId);
        Task<WorkSample?> GetByIdAsync(int id);
        Task AddAsync(WorkSample workSample);
        void Delete(WorkSample workSample);
        Task<bool> SaveChangesAsync();
    }
}
