using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public class WorkSampleRepository : IWorkSampleRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkSampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkSample>> GetByPortfolioIdAsync(int portfolioId)
        {
            return await _context.WorkSamples.Where(w => w.ProtofolioId == portfolioId).ToListAsync();
        }

        public async Task<WorkSample?> GetByIdAsync(int id)
        {
            return await _context.WorkSamples.FindAsync(id);
        }

        public async Task AddAsync(WorkSample workSample)
        {
            await _context.WorkSamples.AddAsync(workSample);
        }

        public void Delete(WorkSample workSample)
        {
            _context.WorkSamples.Remove(workSample);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
