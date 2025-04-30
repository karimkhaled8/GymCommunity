using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts(string query,int page,int eleNo,int? categoryId,int? brandId,string sort,decimal?minPrice,decimal?maxPrice);
        public Task<IEnumerable<ProductDTO>> SearchProducts(string name);
        public Task<int> GetTotalCount(string query,int? categoryId,int? productId,decimal? minPrice,decimal?maxPrice);

        Task<IEnumerable<ProductDTO>> GetUserProducts(string userId);
        Task<ProductDTO?> GetProductById(int productId);
        Task<ProductDTO?> CreateProduct(ProductDTO productDto, string userId);
        Task<ProductDTO?> UpdateProduct(int productId, ProductDTO productDto);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetProductsByBrand(int brandId);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);//filter by category 
        //filter by price                    
        Task<IEnumerable<ProductDTO>> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice);
        Task<IEnumerable<ProductDTO>> GetProductsByPriceRangeAndCategory(int? categoryId, decimal? minPrice, decimal? maxPrice);
    }
}
