using Gym_Community.API.DTOs.Forum;

namespace Gym_Community.Application.Interfaces.Forum
{
    public interface ICommentService
    {
        Task<CommentReadDTO?> CreateAsync(CommentCreateDTO dto);
        Task<IEnumerable<CommentReadDTO>> GetAllAsync();
        Task<CommentReadDTO?> GetByIdAsync(int id);
        Task<CommentReadDTO?> UpdateAsync(int id, CommentCreateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CommentReadDTO>> GetByUserIdAsync(string userId);
        Task<IEnumerable<CommentReadDTO>> GetByPostIdAsync(int postId);
    }
}
