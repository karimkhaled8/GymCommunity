using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IReviewRepository
    {
        public Task<Review?> AddAsync(Review review);
        public Task<IEnumerable<Review>> ListAsync();
        public Task<Review?> GetById(int id);
        public Task<Review?> UpdateAsync(Review review);
        public Task<bool> RemoveAsync(Review review);
    }
}
