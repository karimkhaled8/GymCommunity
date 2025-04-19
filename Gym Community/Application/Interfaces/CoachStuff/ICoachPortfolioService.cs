using Gym_Community.API.DTOs.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface ICoachPortfolioService
    {
        Task<IEnumerable<CoachPortfolioDto>> GetAllAsync();
        Task<CoachPortfolioDto?> GetByIdAsync(int id);
        Task<int> GetIdByportfolioIdAsync(string coachId);
        Task<CoachPortfolioDto?> GetByCoachIdAsync(string coachId);
        Task<bool> CreateAsync(CoachPortfolioDto dto);
        Task<bool> UpdateAsync(int id, CoachPortfolioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
