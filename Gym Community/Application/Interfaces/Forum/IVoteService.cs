using Gym_Community.API.DTOs.Forum;
using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Application.Interfaces.Forum
{
    public interface IVoteService
    {
        Task<VoteReadDTO?> CreateAsync(VoteCreateDTO dto);
        Task<IEnumerable<VoteReadDTO>> GetAllAsync();
        Task<VoteReadDTO?> GetByIdAsync(int id);
        Task<VoteReadDTO?> UpdateAsync(int id, VoteCreateDTO voteUpdateDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<VoteReadDTO>> GetVotesByPostIdAsync(int postId);
        Task<IEnumerable<VoteReadDTO>> GetVotesByCommentIdAsync(int commentId);
        Task<IEnumerable<VoteReadDTO>> GetVotesByUserIdAsync(string userId);
    }
}
