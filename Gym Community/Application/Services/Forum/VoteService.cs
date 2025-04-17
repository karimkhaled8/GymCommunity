using Gym_Community.API.DTOs.Forum;
using Gym_Community.Application.Interfaces.Forum;
using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Microsoft.Extensions.Hosting;

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
            return created != null ? await GetByIdAsync(vote.Id) : null;
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
        public async Task<VoteReadDTO?> UpdateAsync(int id, VoteCreateDTO voteUpdateDTO)
        {
            var vote = await _repo.GetByIdAsync(id);
            if (vote == null) return null;

            vote.IsUpvote = voteUpdateDTO.IsUpvote;
            var updatedVote = await _repo.UpdateAsync(vote);
            return updatedVote == null ? null : ToReadDTO(updatedVote);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var vote = await _repo.GetByIdAsync(id);
            return vote != null && await _repo.DeleteAsync(vote);
        }
        public async Task<IEnumerable<VoteReadDTO>> GetVotesByPostIdAsync(int postId)
        {
            var votes = await _repo.GetVotesByPostIdAsync(postId);
            return votes.Select(ToReadDTO);
        }

        public async Task<IEnumerable<VoteReadDTO>> GetVotesByCommentIdAsync(int commentId)
        {
            var votes = await _repo.GetVotesByCommentIdAsync(commentId);
            return votes.Select(ToReadDTO);
        }
        public async Task<IEnumerable<VoteReadDTO>> GetVotesByUserIdAsync(string userId)
        {
            var votes = await _repo.GetVotesByUserIdAsync(userId);
            return votes.Select(ToReadDTO);
        }
        private VoteReadDTO ToReadDTO(Vote vote)
        {
            return new VoteReadDTO
            {
                Id = vote.Id,
                UserId = vote.UserId,
                PostId = vote.PostId,
                CommentId = vote.CommentId,
                UserName = vote.AppUser.FirstName + " " + vote.AppUser.LastName ?? "",
                IsUpvote = vote.IsUpvote
            };
        }

    }
}
