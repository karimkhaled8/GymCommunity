using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gym_Community.Application.Services.Gym
{
    public class GymImgService:IGymImgService
    {
        private readonly IGymImgRepository _repo;

        public GymImgService(IGymImgRepository repo)
        {
            _repo = repo;
        }

        public async Task<GymImgReadDTO?> CreateAsync(GymImgCreateDTO dto)
        {
            var entity = new GymImgs
            {
                GymId = dto.GymId,
                ImageUrl = dto.ImageUrl??""
            };

            var created = await _repo.AddAsync(entity);
            return created != null ? await GetByIdAsync(created.Id) : null;
        }

        public async Task<IEnumerable<GymImgReadDTO>> GetAllAsync()
        {
            var imgs = await _repo.ListAsync();
            return imgs.Select(ToReadDTO);
        }

        public async Task<GymImgReadDTO?> GetByIdAsync(int id)
        {
            var img = await _repo.GetByIdAsync(id);
            return img != null ? ToReadDTO(img) : null;
        }

        public async Task<IEnumerable<GymImgReadDTO>> GetByGymIdAsync(int gymId)
        {
            var imgs = await _repo.GetByGymIdAsync(gymId);
            return imgs.Select(ToReadDTO);
        }

        public async Task<GymImgReadDTO?> UpdateAsync(int id, GymImgCreateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.GymId = dto.GymId;
            existing.ImageUrl = dto.ImageUrl?? "";

            var updated = await _repo.UpdateAsync(existing);
            return updated != null ? await GetByIdAsync(updated.Id) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var img = await _repo.GetByIdAsync(id);
            return img != null && await _repo.DeleteAsync(img);
        }

        private GymImgReadDTO ToReadDTO(GymImgs img)
        {
            return new GymImgReadDTO
            {
                Id = img.Id,
                GymId = img.GymId,
                ImageUrl = img.ImageUrl
            };
        }
    }
}
