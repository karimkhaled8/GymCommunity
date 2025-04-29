using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Data.Models.E_comms;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IProductRepository
    {
        public Task<Product?> AddAsync(Product product);
        public Task<IEnumerable<Product>> ListAsync(string query="", int page=1, int eleNo=8, string sort = "asc", int? categoryId=null, int? brandId=null, decimal? minPrice=null, decimal? maxPrice = null);
        public Task<int> GetTotalCount(string query, int? categoryId, int? brandId, decimal? minPrice, decimal? maxPrice);
        public Task<IEnumerable<Product>> ListAsync(string name);
        public Task<IEnumerable<Product>> ListUserAsync(string userId);
        public Task<Product?> GetById(int id);
        public Task<Product?> UpdateAsync(Product product);
        public Task<bool> RemoveAsync(Product product);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);//filter by category
                                                                              // In IProductRepository.cs
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAndCategoryAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
    }
}
