using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO?> GetProductById(int productId);
        Task<int> CreateProduct(ProductDTO productDto);
        Task<bool> UpdateProduct(int productId, ProductDTO productDto);
        Task<bool> DeleteProduct(int productId);
    }
}
