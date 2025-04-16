using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public class CoachCertificateRepository :ICoachCertificateRepository
    {
        private readonly ApplicationDbContext _context;
        public CoachCertificateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoachCertificate>> GetByPortfolioIdAsync(int portfolioId)
        {
            return await _context.CoachCertificates
                .Where(c => c.ProtofolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<CoachCertificate?> GetByIdAsync(int id)
        {
            return await _context.CoachCertificates.FindAsync(id);
        }

        public async Task AddAsync(CoachCertificate certificate)
        {
            await _context.CoachCertificates.AddAsync(certificate);
        }

        public void Delete(CoachCertificate certificate)
        {
            _context.CoachCertificates.Remove(certificate);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
