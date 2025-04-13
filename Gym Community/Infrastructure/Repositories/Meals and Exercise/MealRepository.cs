using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Meals_and_Exercise
{
    public class MealRepository : IMealRepository
    {
        private readonly ApplicationDbContext _context;

        public MealRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await _context.Set<Meal>().ToListAsync();
        }

        public async Task<Meal?> GetByIdAsync(int id)
        {
            return await _context.Set<Meal>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Meal meal)
        {
            await _context.Set<Meal>().AddAsync(meal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meal meal)
        {
            _context.Set<Meal>().Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Meal>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Meal>> GetByNameAsync(string name, bool? isSupplement = null)
        {
            var query = _context.Set<Meal>().AsQueryable(); 

            query = query.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

         
            if (isSupplement.HasValue)
            {
                query = query.Where(m => m.IsSupplement == isSupplement.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetBySupplementStatusAsync(bool isSupplement)
        {
            return await _context.Set<Meal>()
                .Where(m => m.IsSupplement == isSupplement)
                .ToListAsync();
        }
    }

}

