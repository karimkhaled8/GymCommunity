using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces.CoachStuff;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;

namespace Gym_Community.Application.Services.CoachStuff
{
    public class CoachRatingService : ICoachRatingService
    {
        private readonly ICoachRatingRepository _repo;

        public CoachRatingService(ICoachRatingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CoachRatingDto>> GetByCoachIdAsync(string coachId)
        {
            var ratings = await _repo.GetByCoachIdAsync(coachId);
            return ratings.Select(r => new CoachRatingDto
            {
                Id = r.Id,
                ClientId = r.ClientId,
                CoachId = r.CoachId,
                Rate = r.Rate,
                Comment = r.Comment
            });
        }

        public async Task<bool> CreateAsync(CoachRatingDto dto)
        {
            var entity = new CoachRating
            {
                ClientId = dto.ClientId,
                CoachId = dto.CoachId,
                Rate = dto.Rate,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
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
