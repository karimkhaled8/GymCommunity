using System.Security.Claims;
using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;

namespace Gym_Community.Application.Services.CoachStuff
{
    public class CoachPortfolioService :ICoachPortfolioService
    { 
        private readonly ICoachPortfolioRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CoachPortfolioService(ICoachPortfolioRepository repository , IHttpContextAccessor httpContextAccessor)
        {
            _repo = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<CoachPortfolioDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(p => new CoachPortfolioDto
            {
                Id = p.Id,
                coachFirstName = p.Coach.FirstName,
                coachLastName = p.Coach.LastName,
                gender = p.Coach.Gender,
                CoachId = p.CoachId,
                AboutMeImageUrl = p.AboutMeImageUrl,
                AboutMeDescription = p.AboutMeDescription,
                Qualifications = p.Qualifications,
                ExperienceYears = p.ExperienceYears,
                SkillsJson = p.SkillsJson,
                SocialMediaLinksJson = p.SocialMediaLinksJson
            });
        }

        public async Task<CoachPortfolioDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new CoachPortfolioDto
            {
                Id = p.Id,
                CoachId = p.CoachId,
                coachFirstName = p.Coach.FirstName,
                coachLastName = p.Coach.LastName,
                gender = p.Coach.Gender,
                AboutMeImageUrl = p.AboutMeImageUrl,
                AboutMeDescription = p.AboutMeDescription,
                Qualifications = p.Qualifications,
                ExperienceYears = p.ExperienceYears,
                SkillsJson = p.SkillsJson,
                SocialMediaLinksJson = p.SocialMediaLinksJson
            };
        }

        public async Task<CoachPortfolioDto?> GetByCoachIdAsync(string coachId)
        {
            var p = await _repo.GetByCoachIdAsync(coachId);
            if (p == null) return null;

            return new CoachPortfolioDto
            {
                Id = p.Id,
                CoachId = p.CoachId,
                coachFirstName = p.Coach.FirstName,
                coachLastName = p.Coach.LastName,
                gender = p.Coach.Gender,
                AboutMeImageUrl = p.AboutMeImageUrl,
                AboutMeDescription = p.AboutMeDescription,
                Qualifications = p.Qualifications,
                ExperienceYears = p.ExperienceYears,
                SkillsJson = p.SkillsJson,
                SocialMediaLinksJson = p.SocialMediaLinksJson
            };
        }

        public async Task<bool> CreateAsync(CoachPortfolioDto dto)
        {
           

            var entity = new CoachPortfolio
            {
                CoachId = dto.CoachId,
                AboutMeImageUrl = dto.AboutMeImageUrl,
                AboutMeDescription = dto.AboutMeDescription,
                Qualifications = dto.Qualifications,
                ExperienceYears = dto.ExperienceYears,
                SkillsJson = dto.SkillsJson,
                SocialMediaLinksJson = dto.SocialMediaLinksJson
            };

            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, CoachPortfolioDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.AboutMeImageUrl = dto.AboutMeImageUrl;
            entity.AboutMeDescription = dto.AboutMeDescription;
            entity.Qualifications = dto.Qualifications;
            entity.ExperienceYears = dto.ExperienceYears;
            entity.SkillsJson = dto.SkillsJson;
            entity.SocialMediaLinksJson = dto.SocialMediaLinksJson;

            _repo.Update(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<int> GetPortfolioIdByCoachIdAsync(string coachId)
        {
            return await _repo.GetPortfolioIdByCoachIdAsync(coachId);

        }
    }
}
