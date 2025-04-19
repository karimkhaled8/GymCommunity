using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Forum
{
    public class CommentRepository:ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment?> AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            return await _context.SaveChangesAsync() > 0 ? comment : null;
        }

        public async Task<IEnumerable<Comment>> ListAsync()
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Include(c => c.Post)
                .Include(c => c.Votes).OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Include(c => c.Post)
                .Include(c => c.Votes).OrderByDescending(p => p.CreatedAt)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            return await _context.SaveChangesAsync() > 0 ? comment : null;
        }

        public async Task<bool> DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Comment>> GetByUserIdAsync(string userId)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Include(c => c.Post)
                .Include(c => c.Votes).OrderByDescending(p => p.CreatedAt)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.AppUser)
                .Include(c => c.Post)
                .Include(c => c.Votes).OrderByDescending(p => p.CreatedAt)
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }
    }
}
