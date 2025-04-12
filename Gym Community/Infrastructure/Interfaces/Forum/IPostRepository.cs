﻿using Gym_Community.Domain.Models.Forum;

namespace Gym_Community.Infrastructure.Interfaces.Forum
{
    public interface IPostRepository
    {
        Task<Post?> AddAsync(Post post);
        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> ListAsync();
        Task<Post?> UpdateAsync(Post post);
        Task<bool> DeleteAsync(Post post);
    }
}
