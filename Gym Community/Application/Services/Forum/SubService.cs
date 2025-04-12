using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces.Forum;

namespace Gym_Community.Application.Services.Forum
{
    public class SubService:ISubService
    {
        private readonly ISubRepository _repo;

        public SubService(ISubRepository repo)
        {
            _repo = repo;
        }

        public async Task<SubReadDTO?> CreateAsync(SubCreateDTO dto)
        {
            var sub = new Sub
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var created = await _repo.AddAsync(sub);
            return created != null ? ToReadDTO(created) : null;
        }

        public async Task<IEnumerable<SubReadDTO>> GetAllAsync()
        {
            var subs = await _repo.ListAsync();
            return subs.Select(s => ToReadDTO(s));
        }

        public async Task<SubReadDTO?> GetByIdAsync(int id)
        {
            var sub = await _repo.GetByIdAsync(id);
            return sub != null ? ToReadDTO(sub) : null;
        }

        public async Task<SubReadDTO?> UpdateAsync(int id, SubCreateDTO dto)
        {
            var sub = await _repo.GetByIdAsync(id);
            if (sub == null) return null;

            sub.Name = dto.Name;
            sub.Description = dto.Description;

            var updated = await _repo.UpdateAsync(sub);
            return updated != null ? ToReadDTO(updated) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sub = await _repo.GetByIdAsync(id);
            return sub != null && await _repo.DeleteAsync(sub);
        }

        private SubReadDTO ToReadDTO(Sub sub)
        {
            return new SubReadDTO
            {
                Id = sub.Id,
                Name = sub.Name,
                Description = sub.Description,
                PostCount = sub.Posts?.Count ?? 0
            };
        }
    }
}
