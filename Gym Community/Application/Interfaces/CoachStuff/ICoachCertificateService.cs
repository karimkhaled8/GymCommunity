using Gym_Community.API.DTOs.Coach.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface ICoachCertificateService
    {
        Task<IEnumerable<CoachCertificateDto>> GetByPortfolioIdAsync(int portfolioId);
        Task<bool> CreateAsync(CoachCertificateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
