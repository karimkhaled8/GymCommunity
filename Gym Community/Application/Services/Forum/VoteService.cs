using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces.Forum;

namespace Gym_Community.Application.Services.Forum
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _repo;

        public VoteService(IVoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<VoteReadDTO?> CreateAsync(VoteCreateDTO dto)
        {
            var vote = new Vote
            {
                UserId = dto.UserId,
                PostId = dto.PostId,
                CommentId = dto.CommentId,
                IsUpvote = dto.IsUpvote
            };

            var created = await _repo.AddAsync(vote);
            return created != null ? ToReadDTO(created) : null;
        }

        public async Task<IEnumerable<VoteReadDTO>> GetAllAsync()
        {
            var votes = await _repo.ListAsync();
            return votes.Select(v => ToReadDTO(v));
        }

        public async Task<VoteReadDTO?> GetByIdAsync(int id)
        {
            var vote = await _repo.GetByIdAsync(id);
            return vote != null ? ToReadDTO(vote) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vote = await _repo.GetByIdAsync(id);
            return vote != null && await _repo.DeleteAsync(vote);
        }

        private VoteReadDTO ToReadDTO(Vote vote)
        {
            return new VoteReadDTO
            {
                Id = vote.Id,
                UserId = vote.UserId,
                PostId = vote.PostId,
                CommentId = vote.CommentId,
                IsUpvote = vote.IsUpvote
            };
        }

    }
}
