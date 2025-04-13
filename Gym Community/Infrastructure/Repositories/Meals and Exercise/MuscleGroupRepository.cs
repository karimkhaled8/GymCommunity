using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories
{
    public class MuscleGroupRepository : IMuscleGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public MuscleGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllAsync()
        {
            return await _context.Set<MuscleGroup>()
                .Include(mg => mg.Exercises)
                .ToListAsync();
        }

        public async Task<MuscleGroup?> GetByIdAsync(int id)
        {
            return await _context.Set<MuscleGroup>()
                .Include(mg => mg.Exercises)
                .FirstOrDefaultAsync(mg => mg.Id == id);
        }

        public async Task AddAsync(MuscleGroup muscleGroup)
        {
            await _context.Set<MuscleGroup>().AddAsync(muscleGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MuscleGroup muscleGroup)
        {
            _context.Set<MuscleGroup>().Update(muscleGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<MuscleGroup>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
