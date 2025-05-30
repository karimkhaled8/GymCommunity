﻿using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IBrandService
    {
        Task<BrandDTO> CreateBrand(BrandDTO brandDto);
        Task<IEnumerable<BrandDTO>> GetAllBrands();
        Task<IEnumerable<BrandDTO>> GetFilteredBrands(string? nameFilter); // Add this
        Task<BrandDTO?> GetBrandById(int brandId);
        Task<BrandDTO?> UpdateBrand(int brandId, BrandDTO brandDto);
        Task<bool> DeleteBrand(int brandId);
    }
}
