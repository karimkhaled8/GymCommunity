using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Meals_and_Exercise
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _context.Set<Exercise>()
                .Include(e => e.MuscleGroup)
                .ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _context.Set<Exercise>()
                .Include(e => e.MuscleGroup)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exercise>> GetByMuscleGroupAsync(int muscleGroupId)
        {
            return await _context.Set<Exercise>()
                .Where(e => e.MuscleGroupId == muscleGroupId)
                .Include(e => e.MuscleGroup)
                .ToListAsync();
        }

        public async Task AddAsync(Exercise exercise)
        {
            await _context.Set<Exercise>().AddAsync(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            _context.Set<Exercise>().Update(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Exercise>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
    }
