using Gym_Community.Domain.Data.Models.E_comms;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;   
        }

        public async Task<Product?> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            if (await _context.SaveChangesAsync() > 0)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products
                //.Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> ListAsync(string name)
        {
            return await _context.Products
                .Where(p=>p.Name.Contains(name))
                //.Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                //.Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product?> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            if (await _context.SaveChangesAsync() > 0)
            {
                return product;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> RemoveAsync(Product product)
        {
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            if (await _context.SaveChangesAsync() > 0)
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
