using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Infrastructure.Interfaces.Forum
{
    public interface ICommentRepository
    {
        Task<Comment?> AddAsync(Comment comment);
        Task<IEnumerable<Comment>> ListAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(Comment comment);
    }
}
