using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public class CoachOffersRepository:ICoachOffersRepository
    {
        private readonly ApplicationDbContext _context;

        public CoachOffersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoachOffers>> GetAllAsync()
        {
            return await _context.CoachOffers.Include(o => o.Coach).ToListAsync();
        }

        public async Task<IEnumerable<CoachOffers>> GetByCoachIdAsync(string coachId)
        {
            return await _context.CoachOffers
                .Include(o => o.Coach)
                .Where(o => o.CoachId == coachId)
                .ToListAsync();
        }

        public async Task<CoachOffers> GetByIdWithCoachAsync(int id)
        {
            return await _context.CoachOffers
                .Include(o => o.Coach)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

      
        public async Task<CoachOffers> AddAsync(CoachOffers offer)
        {
            _context.CoachOffers.Add(offer);
            await _context.SaveChangesAsync();

            return await GetByIdWithCoachAsync(offer.Id); // Return with Coach info
        }

        public async Task<CoachOffers> UpdateAsync(CoachOffers offer)
        {
            _context.CoachOffers.Update(offer);
            await _context.SaveChangesAsync();

            return await GetByIdWithCoachAsync(offer.Id); // Return updated + Coach
        }

        public async Task DeleteAsync(int id)
        {
            var offer = await _context.CoachOffers.FindAsync(id);
            if (offer != null)
            {
                _context.CoachOffers.Remove(offer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
