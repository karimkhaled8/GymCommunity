using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Brand?> AddAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            if (await _context.SaveChangesAsync() > 0)
            {
                return brand;
            }
            else return null;
        }
        public async Task<IEnumerable<Brand>> ListAsync()
        {
            return await _context.Brands.ToListAsync(); 
        }
        public async Task<Brand?> GetById(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.BrandID == id);
        }
        public async Task<Brand?> UpdateAsync(Brand brand)
        {
            _context.Brands.Update(brand); 
            if(await _context.SaveChangesAsync() > 0)
            {
                return brand;
            }
            else
            {
                return null; 
            }
        }

        public async Task<bool> RemoveAsync(Brand brand)
        {
            _context.Brands.Remove(brand);
            if( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
    }
}
