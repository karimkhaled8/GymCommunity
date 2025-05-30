﻿using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Forum
{
    public class VoteRepository:IVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vote?> AddAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            return await _context.SaveChangesAsync() > 0 ? vote : null;
        }

        public async Task<IEnumerable<Vote>> ListAsync()
        {
            return await _context.Votes.Include(v => v.AppUser).ToListAsync();
        }

        public async Task<Vote?> GetByIdAsync(int id)
        {
            return await _context.Votes.Include(v => v.AppUser)
                .Where(v => v.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Vote?> UpdateAsync(Vote vote)
        {
            _context.Votes.Update(vote);
            await _context.SaveChangesAsync();
            return vote;
        }
        public async Task<bool> DeleteAsync(Vote vote)
        {
            _context.Votes.Remove(vote);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Vote>> GetVotesByPostIdAsync(int postId)
        {
            return await _context.Votes.Include(v => v.AppUser).Where(v => v.PostId == postId).ToListAsync();
        }

        public async Task<IEnumerable<Vote>> GetVotesByCommentIdAsync(int commentId)
        {
            return await _context.Votes.Include(v => v.AppUser).Where(v => v.CommentId == commentId).ToListAsync();
        }
        public async Task<IEnumerable<Vote>> GetVotesByUserIdAsync(string userId)
        {
            return await _context.Votes.Include(v => v.AppUser).Where(v => v.UserId == userId).ToListAsync();
        }
    }
}
