using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces.Forum;
using static System.Net.Mime.MediaTypeNames;

namespace Gym_Community.Application.Services.Forum
{
    public class PostService: IPostService
    {
        private readonly IPostRepository _repo;
        private readonly IAwsService _awsService;


        public PostService(IPostRepository repo, IAwsService awsService)
        {
            _repo = repo;
            _awsService = awsService;
        }

        public async Task<PostReadDTO?> CreateAsync(PostCreateDTO dto)
        {
            var post = new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                imgUrl = dto.ImgUrl ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId,
                SubId = dto.SubId
            };

            var created = await _repo.AddAsync(post);
            return created != null ? await ToReadDTOAsync(created.Id) : null;
        }

        public async Task<IEnumerable<PostReadDTO>> GetAllAsync()
        {
            var posts = await _repo.ListAsync();
            return posts.Select(p => new PostReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImgUrl = p.imgUrl,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId,
                UserName = p.AppUser.UserName ?? "",
                SubId = p.SubId,
                SubName = p.Sub.Name,
                CommentCount = p.Comments?.Count ?? 0,
                VoteCount = p.Votes.Count
            });
        }

        public async Task<PostReadDTO?> GetByIdAsync(int id)
        {
            return await ToReadDTOAsync(id);
        }

        public async Task<PostReadDTO?> UpdateAsync(int id, PostCreateDTO dto)
        {
            var post = await _repo.GetByIdAsync(id);
            if (post == null) return null;

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.imgUrl = dto.ImgUrl ?? string.Empty;
            post.SubId = dto.SubId;

            var updated = await _repo.UpdateAsync(post);
            return updated != null ? await ToReadDTOAsync(updated.Id) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _repo.GetByIdAsync(id);
            return post != null && await _repo.DeleteAsync(post);
        }
        public async Task<IEnumerable<PostReadDTO>> GetByUserIdAsync(string userId)
        {
            var posts = await _repo.GetPostsByUserIdAsync(userId);
            return posts.Select(p => new PostReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImgUrl = p.imgUrl,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId,
                UserName = p.AppUser.UserName ?? "",
                SubId = p.SubId,
                SubName = p.Sub.Name,
                CommentCount = p.Comments?.Count ?? 0,
                VoteCount = p.Votes.Count
            });
        }

        public async Task<IEnumerable<PostReadDTO>> GetBySubIdAsync(int subId)
        {
            var posts = await _repo.GetPostsBySubIdAsync(subId);
            return posts.Select(p => new PostReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                ImgUrl = p.imgUrl,
                CreatedAt = p.CreatedAt,
                UserId = p.UserId,
                UserName = p.AppUser.UserName ?? "",
                SubId = p.SubId,
                SubName = p.Sub.Name,
                CommentCount = p.Comments?.Count ?? 0,
                VoteCount = p.Votes.Count
            });
        }

        private async Task<PostReadDTO?> ToReadDTOAsync(int id)
        {
            var post = await _repo.GetByIdAsync(id);
            return post == null ? null : new PostReadDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImgUrl = post.imgUrl,
                CreatedAt = post.CreatedAt,
                UserId = post.UserId,
                UserName = post.AppUser?.UserName ?? "",
                SubId = post.SubId,
                SubName = post.Sub?.Name ?? "",
                CommentCount = post.Comments?.Count ?? 0,
                VoteCount = post.Votes?.Count ?? 0
            };
        }
    }
}
