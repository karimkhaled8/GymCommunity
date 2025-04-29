using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public class CoachPortfolioRepository :ICoachPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public CoachPortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CoachPortfolio>> GetAllAsync()
        {
            return await _context.CoachPortfolios.Include(c=>c.Coach).ToListAsync();
        }

        public async Task<CoachPortfolio?> GetByIdAsync(int id)
        {
            return await _context.CoachPortfolios.Include(c => c.Coach).FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task<CoachPortfolio?> GetByCoachIdAsync(string coachId)
        {
            return await _context.CoachPortfolios.Include(c => c.Coach).FirstOrDefaultAsync(p => p.CoachId == coachId);
        }

        public async Task AddAsync(CoachPortfolio portfolio)
        {
            await _context.CoachPortfolios.AddAsync(portfolio);
        }

        public void Update(CoachPortfolio portfolio)
        {
            _context.CoachPortfolios.Update(portfolio);
        }

        public void Delete(CoachPortfolio portfolio)
        {
            _context.CoachPortfolios.Remove(portfolio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetPortfolioIdByCoachIdAsync(string coachId)
        {
           return await _context.CoachPortfolios
                .Where(p => p.CoachId == coachId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }
    }
}
