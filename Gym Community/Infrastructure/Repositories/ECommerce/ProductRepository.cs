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
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> ListAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
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

        public async  Task<IEnumerable<Product>> ListUserAsync(string userId)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.Brand)
                .Where(p => p.OwnerId == userId).ToListAsync();
            return products;
        }

        // filter by category

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync();
        }

        // filter by price 

        // In ProductRepository.cs
        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAndCategoryAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }
    }
}
