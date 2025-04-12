using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Forum
{
    public class SubRepository:ISubRepository
    {
        private readonly ApplicationDbContext _context;

        public SubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sub?> AddAsync(Sub sub)
        {
            await _context.Subs.AddAsync(sub);
            return await _context.SaveChangesAsync() > 0 ? sub : null;
        }

        public async Task<IEnumerable<Sub>> ListAsync()
        {
            return await _context.Subs.Include(s => s.Posts).ToListAsync();
        }

        public async Task<Sub?> GetByIdAsync(int id)
        {
            return await _context.Subs.Include(s => s.Posts).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sub?> UpdateAsync(Sub sub)
        {
            _context.Subs.Update(sub);
            return await _context.SaveChangesAsync() > 0 ? sub : null;
        }

        public async Task<bool> DeleteAsync(Sub sub)
        {
            _context.Subs.Remove(sub);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
