using Gym_Community.Domain.Models.Forum;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Forum;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Forum
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post?> AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            return await _context.SaveChangesAsync() > 0 ? post : null;
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.AppUser)
                .Include(p => p.Sub)
                .Include(p => p.Comments)
                .Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> ListAsync()
        {
            return await _context.Posts
                .Include(p => p.AppUser)
                .Include(p => p.Sub)
                .Include(p => p.Comments)
                .Include(p => p.Votes)
                .ToListAsync();
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            return await _context.SaveChangesAsync() > 0 ? post : null;
        }

        public async Task<bool> DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

