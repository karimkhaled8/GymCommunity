using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;

namespace Gym_Community.Application.Services.CoachStuff
{
    public class WorkSampleService :IWorkSampleService
    {
        private readonly IWorkSampleRepository _repo;

        public WorkSampleService(IWorkSampleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WorkSampleDto>> GetByPortfolioIdAsync(int portfolioId)
        {
            var workSamples = await _repo.GetByPortfolioIdAsync(portfolioId);
            return workSamples.Select(w => new WorkSampleDto
            {
                Id = w.Id,
                ImageUrl = w.ImageUrl,
                Description = w.Description,
                ProtofolioId = w.ProtofolioId
            });
        }

        public async Task<bool> CreateAsync(WorkSampleDto dto)
        {
            var entity = new WorkSample
            {
                ImageUrl = dto.ImageUrl,
                Description = dto.Description,
                ProtofolioId = dto.ProtofolioId
            };
            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            return await _repo.SaveChangesAsync();
        }
    }
}
