using Gym_Community.API.DTOs.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface IWorkSampleService
    {
        Task<IEnumerable<WorkSampleDto>> GetByPortfolioIdAsync(int portfolioId);
        Task<bool> CreateAsync(WorkSampleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
