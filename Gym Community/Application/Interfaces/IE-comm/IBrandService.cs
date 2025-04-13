using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IBrandService
    {
        Task<BrandDTO> CreateBrand(BrandDTO brandDto);
        Task<IEnumerable<BrandDTO>> GetAllBrands();
        Task<BrandDTO> GetBrandById(int brandId);
        Task<bool> UpdateBrand(int brandId, BrandDTO brandDto);
        Task<bool> DeleteBrand(int brandId);
    }
}
