using Gym_Community.API.DTOs.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface ICoachCertificateService
    {
        Task<IEnumerable<CoachCertificateDto>> GetByPortfolioIdAsync(int portfolioId);
        Task<bool> CreateAsync(CoachCertificateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
