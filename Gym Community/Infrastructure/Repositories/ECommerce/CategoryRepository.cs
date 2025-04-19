using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);

            if(await _context.SaveChangesAsync()>0)
            {
                return category;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync(); 
        }
        public async Task<Category?> GetById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id); 
        }
        public async Task<Category?> UpdateAsync(Category category)
        {
            _context.Categories.Update(category); 
            if(await _context.SaveChangesAsync()  > 0)
            {
                return category;
            }
            else
            {
                return null; 
            }
        }
        public async Task<bool> RemoveAsync(Category category)
        {
            var categoryy = await _context.Categories
                .Where(c => c.CategoryID == category.CategoryID).FirstOrDefaultAsync();
            if (categoryy != null) { 
              categoryy.IsDeleted = true;
            }
            return await _context.SaveChangesAsync() > 0;
            
        }
    }
}
