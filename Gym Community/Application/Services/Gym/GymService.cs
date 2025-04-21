using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Gym_Community.Application.Services.Gym
{
    public class GymService:IGymService
    {
        private readonly IGymRepository _repo;
        private readonly UserManager<AppUser> _userManager;

        public GymService(IGymRepository repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        public async Task<bool> IsUserGymOwnerAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, "GymOwner");
        }

        public async Task<GymReadDTO?> AddAsync(GymCreateDTO dto)
        {
            if (!await IsUserGymOwnerAsync(dto.OwnerId))
                throw new UnauthorizedAccessException("User must be a GymOwner.");

            var gym = new Gym_Community.Domain.Models.Gym.Gym
            {
                OwnerId = dto.OwnerId,
                Name = dto.Name,
                Location = dto.Location,
                Description = dto.Description,
                PhoneNumber = dto.PhoneNumber,
                Website = dto.Website,
                Email = dto.Email,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CreatedAt = DateTime.UtcNow
            };

            var added = await _repo.AddAsync(gym);
            if (added == null) return null;
            return added != null ? await GetByIdAsync(added.Id) : null;
        }

        public async Task<IEnumerable<GymReadDTO>> ListAsync()
        {
            var gyms = await _repo.ListAsync();
            return gyms.Select(Map);

        }

        public async Task<GymReadDTO?> GetByIdAsync(int id)
        {
            var gym = await _repo.GetByIdAsync(id);
            if (gym == null) return null;
            return gym != null ? Map(gym) : null;
        }

        public async Task<IEnumerable<GymReadDTO>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm)
        {
            var gyms = await _repo.GetNearbyGymsAsync(lat, lng, radiusInKm);
            return gyms.Select(Map);
        }

        public async Task<GymReadDTO?> UpdateAsync(int id, GymCreateDTO dto)
        {
            var gym = await _repo.GetByIdAsync(id);
            if (gym == null) return null;

            gym.Name = dto.Name;
            gym.Location = dto.Location;
            gym.Description = dto.Description;
            gym.PhoneNumber = dto.PhoneNumber;
            gym.Website = dto.Website;
            gym.Email = dto.Email;
            gym.Latitude = dto.Latitude;
            gym.Longitude = dto.Longitude;

            var updated = await _repo.UpdateAsync(gym);
            if (updated == null) return null;

            return updated != null ? Map(updated) : null;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gym = await _repo.GetByIdAsync(id);
            if (gym == null) return false;
            return await _repo.DeleteAsync(gym);
        }

        private GymReadDTO Map(Gym_Community.Domain.Models.Gym.Gym gym)
        {
            return new GymReadDTO
            {
                Id = gym.Id,
                OwnerId = gym.OwnerId,
                Name = gym.Name,
                Location = gym.Location,
                Description = gym.Description,
                PhoneNumber = gym.PhoneNumber,
                Website = gym.Website,
                Email = gym.Email,
                Latitude = gym.Latitude,
                Longitude = gym.Longitude,
                CreatedAt = gym.CreatedAt
            };
        }
    }
}
