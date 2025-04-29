using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        public Task<IEnumerable<ProductDTO>> SearchProducts(string name);
      
        Task<IEnumerable<ProductDTO>> GetUserProducts(string userId);
        Task<ProductDTO?> GetProductById(int productId);
        Task<ProductDTO?> CreateProduct(ProductDTO productDto);
        Task<ProductDTO?> UpdateProduct(int productId, ProductDTO productDto);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetProductsByBrand(int brandId);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);//filter by category 
        //filter by price                    
        Task<IEnumerable<ProductDTO>> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<ProductDTO>> GetProductsByPriceRangeAndCategory(int? categoryId, decimal? minPrice, decimal? maxPrice);
    }
}
