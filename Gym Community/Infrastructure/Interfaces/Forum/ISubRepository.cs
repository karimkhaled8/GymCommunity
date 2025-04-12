using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Infrastructure.Interfaces.Forum
{
    public interface ISubRepository
    {
        Task<Sub?> AddAsync(Sub sub);
        Task<IEnumerable<Sub>> ListAsync();
        Task<Sub?> GetByIdAsync(int id);
        Task<Sub?> UpdateAsync(Sub sub);
        Task<bool> DeleteAsync(Sub sub);
    }
}
