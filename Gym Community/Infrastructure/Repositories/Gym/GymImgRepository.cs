using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class GymImgRepository:IGymImgRepository
    {
        private readonly ApplicationDbContext _context;

        public GymImgRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GymImgs?> AddAsync(GymImgs img)
        {
            await _context.GymImgs.AddAsync(img);
            return await _context.SaveChangesAsync() > 0 ? img : null;
        }

        public async Task<IEnumerable<GymImgs>> ListAsync()
        {
            return await _context.GymImgs.Include(g=>g.Gym).ToListAsync();
        }

        public async Task<GymImgs?> GetByIdAsync(int id)
        {
            return await _context.GymImgs.Include(g => g.Gym).Where(g =>g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GymImgs>> GetByGymIdAsync(int gymId)
        {
            return await _context.GymImgs
                .Where(img => img.GymId == gymId).Include(g => g.Gym)
                .ToListAsync();
        }

        public async Task<GymImgs?> UpdateAsync(GymImgs img)
        {
            _context.GymImgs.Update(img);
            return await _context.SaveChangesAsync() > 0 ? img : null;
        }

        public async Task<bool> DeleteAsync(GymImgs img)
        {
            _context.GymImgs.Remove(img);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
