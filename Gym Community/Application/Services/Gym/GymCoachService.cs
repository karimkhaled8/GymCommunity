using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.AspNetCore.Identity;

namespace Gym_Community.Application.Services.Gym
{
    public class GymCoachService: IGymCoachService
    {
        private readonly IGymCoachRepository _repo;
        private readonly UserManager<AppUser> _userManager;


        public GymCoachService(IGymCoachRepository repo, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        public async Task<bool> IsUserCoachAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            return await _userManager.IsInRoleAsync(user, "Coach");
        }

        public async Task<GymCoachDTO?> CreateAsync(GymCoachCreateDTO dto)
        {
            if (!await IsUserCoachAsync(dto.CoachID))
                throw new UnauthorizedAccessException("User must be a Coach.");

            var entity = new GymCoach
            {
                GymId = dto.GymId,
                CoachID = dto.CoachID
            };

            var created = await _repo.AddAsync(entity);
            return created != null ? ToReadDTO(created) : null;
        }

        public async Task<IEnumerable<GymCoachDTO>> GetAllAsync()
        {
            var list = await _repo.ListAsync();
            return list.Select(gc => ToReadDTO(gc));
        }

        public async Task<GymCoachDTO?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity != null ? ToReadDTO(entity) : null;
        }

        public async Task<GymCoachDTO?> UpdateAsync(int id, GymCoachCreateDTO dto)
        {
            if (!await IsUserCoachAsync(dto.CoachID))
                throw new UnauthorizedAccessException("User must be a Coach.");

            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.GymId = dto.GymId;
            entity.CoachID = dto.CoachID;

            var updated = await _repo.UpdateAsync(entity);
            return updated != null ? ToReadDTO(updated) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity != null && await _repo.DeleteAsync(entity);
        }

        public async Task<IEnumerable<GymCoachDTO>> GetByGymIdAsync(int gymId)
        {
            var list = await _repo.GetByGymId(gymId);
            return list.Select(gc => ToReadDTO(gc));
        }

        private GymCoachDTO ToReadDTO(GymCoach gc)
        {
            return new GymCoachDTO
            {
                Id = gc.Id,
                GymId = gc.GymId,
                CoachID = gc.CoachID,
                CoachName = gc.Coach?.FirstName ?? "Unknown",
                GymName = gc.Gym?.Name,
                
            };
        }
    }
}
