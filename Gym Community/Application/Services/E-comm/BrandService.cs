using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BrandDTO?> CreateBrand(BrandDTO brandDto)
        {
            var newBrand = new Brand
            {
                Name = brandDto.Name,
                Description = brandDto.Description
            };

            var result = await _brandRepository.AddAsync(newBrand);

            return result == null ? null : new BrandDTO
            {
                BrandID = result.BrandID,
                Name = result.Name,
                Description = result.Description
            };
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrands()
        {
            var brands = await _brandRepository.ListAsync();
            return brands.Select(b => new BrandDTO
            {
                BrandID = b.BrandID,
                Name = b.Name,
                Description = b.Description
            }).ToList();
        }

        public async Task<IEnumerable<BrandDTO>> GetFilteredBrands(string? nameFilter)
        {
            var brands = await _brandRepository.GetFilteredBrandsAsync(nameFilter);
            return brands.Select(b => new BrandDTO
            {
                BrandID = b.BrandID,
                Name = b.Name,
                Description = b.Description
            });
        }

        public async Task<BrandDTO?> GetBrandById(int brandId)
        {
            var brand = await _brandRepository.GetById(brandId);

            return brand == null ? null : new BrandDTO
            {
                BrandID = brand.BrandID,
                Name = brand.Name,
                Description = brand.Description
            };
        }

        public async Task<BrandDTO?> UpdateBrand(int brandId, BrandDTO brandDto)
        {
            var existing = await _brandRepository.GetById(brandId);
            
            if (existing == null) return null;

            existing.Name = brandDto.Name;
            existing.Description = brandDto.Description;

            var updated = await _brandRepository.UpdateAsync(existing);

            return updated == null ? null : new BrandDTO
            {
                BrandID = updated.BrandID,
                Name = updated.Name,
                Description = updated.Description
            };  
        }

        public async Task<bool> DeleteBrand(int brandId)
        {
            var brand = await _brandRepository.GetById(brandId);
            if (brand == null) return false;

            return await _brandRepository.RemoveAsync(brand);
        }
    }
}
