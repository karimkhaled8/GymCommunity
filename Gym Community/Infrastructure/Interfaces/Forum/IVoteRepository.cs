using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Infrastructure.Interfaces.Forum
{
    public interface IVoteRepository
    {
        Task<Vote?> AddAsync(Vote vote);
        Task<IEnumerable<Vote>> ListAsync();
        Task<Vote?> GetByIdAsync(int id);
        Task<Vote?> UpdateAsync(Vote vote);
        Task<bool> DeleteAsync(Vote vote);
        Task<IEnumerable<Vote>> GetVotesByPostIdAsync(int postId);
        Task<IEnumerable<Vote>> GetVotesByCommentIdAsync(int commentId);
        Task<IEnumerable<Vote>> GetVotesByUserIdAsync(string userId);  


    }
}
