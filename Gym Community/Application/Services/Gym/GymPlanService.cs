using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Interfaces.Gym;

namespace Gym_Community.Application.Services.Gym
{
    public class GymPlanService:IGymPlanService
    {
        private readonly IGymPlanRepository _repo;

        public GymPlanService(IGymPlanRepository repo)
        {
            _repo = repo;
        }

        public async Task<GymPlanReadDTO?> CreateAsync(GymPlanCreateDTO dto)
        {
            var plan = new GymPlan
            {
                GymId = dto.GymId,
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                DurationMonths = dto.DurationMonths,
                HasPrivateCoach = dto.HasPrivateCoach,
                HasNutritionPlan = dto.HasNutritionPlan,
                HasAccessToAllAreas = dto.HasAccessToAllAreas
            };

            var created = await _repo.AddAsync(plan);
            return created != null ? await GetByIdAsync(created.Id) : null;
        }

        public async Task<IEnumerable<GymPlanReadDTO>> GetAllAsync()
        {
            var plans = await _repo.ListAsync();
            return plans.Select(p => ToReadDTO(p));
        }

        public async Task<GymPlanReadDTO?> GetByIdAsync(int id)
        {
            var plan = await _repo.GetByIdAsync(id);
            return plan != null ? ToReadDTO(plan) : null;
        }

        public async Task<IEnumerable<GymPlanReadDTO>> GetByGymIdAsync(int gymId)
        {
            var plans = await _repo.GetByGymIdAsync(gymId);
            return plans.Select(p => ToReadDTO(p));
        }

        public async Task<GymPlanReadDTO?> UpdateAsync(int id, GymPlanCreateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.GymId = dto.GymId;
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.DurationMonths = dto.DurationMonths;
            existing.HasPrivateCoach = dto.HasPrivateCoach;
            existing.HasNutritionPlan = dto.HasNutritionPlan;
            existing.HasAccessToAllAreas = dto.HasAccessToAllAreas;

            var updated = await _repo.UpdateAsync(existing);
            return updated != null ? await GetByIdAsync(updated.Id) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _repo.GetByIdAsync(id);
            return plan != null && await _repo.DeleteAsync(plan);
        }

        private GymPlanReadDTO ToReadDTO(GymPlan plan)
        {
            return new GymPlanReadDTO
            {
                Id = plan.Id,
                GymId = plan.GymId,
                Title = plan.Title,
                Description = plan.Description,
                Price = plan.Price,
                DurationMonths = plan.DurationMonths,
                HasPrivateCoach = plan.HasPrivateCoach,
                HasNutritionPlan = plan.HasNutritionPlan,
                HasAccessToAllAreas = plan.HasAccessToAllAreas,
                NoOfSubscriptions = plan.UserSubscriptions?.Count ?? 0
            };
        }
    }
}
