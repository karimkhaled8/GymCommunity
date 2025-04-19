using Gym_Community.Domain.Models.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface ICoachCertificateRepository
    {
        Task<IEnumerable<CoachCertificate>> GetByPortfolioIdAsync(int portfolioId);
        Task<CoachCertificate?> GetByIdAsync(int id);
        Task AddAsync(CoachCertificate certificate);
        void Delete(CoachCertificate certificate);
        Task<bool> SaveChangesAsync();
    }
}
