using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces.Forum;

namespace Gym_Community.Application.Services.Forum
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _repo;

        public CommentService(ICommentRepository repo)
        {
            _repo = repo;
        }

        public async Task<CommentReadDTO?> CreateAsync(CommentCreateDTO dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                UserId = dto.UserId,
                PostId = dto.PostId,
                CreatedAt = DateTime.Now,

            };

            var created = await _repo.AddAsync(comment);
            return created != null ? ToReadDTO(created) : null;
        }

        public async Task<IEnumerable<CommentReadDTO>> GetAllAsync()
        {
            var comments = await _repo.ListAsync();
            return comments.Select(ToReadDTO);
        }

        public async Task<CommentReadDTO?> GetByIdAsync(int id)
        {
            var comment = await _repo.GetByIdAsync(id);
            return comment != null ? ToReadDTO(comment) : null;
        }

        public async Task<CommentReadDTO?> UpdateAsync(int id, CommentCreateDTO dto)
        {
            var comment = await _repo.GetByIdAsync(id);
            if (comment == null) return null;

            comment.Content = dto.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            var updated = await _repo.UpdateAsync(comment);
            return updated != null ? ToReadDTO(updated) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _repo.GetByIdAsync(id);
            return comment != null && await _repo.DeleteAsync(comment);
        }

        private CommentReadDTO ToReadDTO(Comment comment)
        {
            return new CommentReadDTO
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt,
                UserId = comment.UserId,
                PostId = comment.PostId,
                VoteCount = comment.Votes.Count

            };
        }
    }
}
