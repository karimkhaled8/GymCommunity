using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetReviews();
        Task<ReviewDTO?> GetReviewById(int reviewId);
        Task<int> CreateReview(ReviewDTO reviewDto);
        Task<bool> UpdateReview(int reviewId, ReviewDTO reviewDto);
        Task<bool> DeleteReview(int reviewId);
        Task<IEnumerable<ReviewDTO>> GetProductReviews(int productId);
    }
}
