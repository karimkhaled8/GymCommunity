using Gym_Community.API.DTOs.Coach.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface ICoachPortfolioService
    {
        Task<IEnumerable<CoachPortfolioDto>> GetAllAsync();
        Task<CoachPortfolioDto?> GetByIdAsync(int id);
        Task<int> GetPortfolioIdByCoachIdAsync(string coachId);
        Task<CoachPortfolioDto?> GetByCoachIdAsync(string coachId);
        Task<bool> CreateAsync(CoachPortfolioDto dto);
        Task<bool> UpdateAsync(int id, CoachPortfolioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
