using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviews()
        {
            var reviews = await _reviewRepository.ListAsync();
            var reviewDtos = reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                ProductID = r.ProductID,
                UserID = r.UserID,
                ProductName = r.Product?.Name,
                UserName = r.AppUser?.UserName
            });

            return reviewDtos;
        }

        public async Task<ReviewDTO?> GetReviewById(int reviewId)
        {
            var review = await _reviewRepository.GetById(reviewId);
            if (review == null) return null;

            var reviewDto = new ReviewDTO
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                ProductID = review.ProductID,
                UserID = review.UserID,
                ProductName = review.Product?.Name,
                UserName = review.AppUser?.UserName
            };

            return reviewDto;
        }

        public async Task<int> CreateReview(ReviewDTO reviewDto)
        {
            var review = new Review
            {
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow,
                ProductID = reviewDto.ProductID,
                UserID = reviewDto.UserID
            };

            var addedReview = await _reviewRepository.AddAsync(review);

            // Debugging line to ensure the review is created
            if (addedReview != null)
            {
                Console.WriteLine($"Review created successfully for product {reviewDto.ProductID}");
            }
            else
            {
                Console.WriteLine("Failed to create review");
            }

            return addedReview?.Id ?? 0;
        }

        public async Task<bool> UpdateReview(int reviewId, ReviewDTO reviewDto)
        {
            var existingReview = await _reviewRepository.GetById(reviewId);
            if (existingReview == null) return false;

            existingReview.Rating = reviewDto.Rating;
            existingReview.Comment = reviewDto.Comment;
            existingReview.ProductID = reviewDto.ProductID;
            existingReview.UserID = reviewDto.UserID;

            await _reviewRepository.UpdateAsync(existingReview);
            return true;
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            var review = await _reviewRepository.GetById(reviewId);
            if (review == null) return false;

            await _reviewRepository.RemoveAsync(review);
            return true;
        }

        public async Task<IEnumerable<ReviewDTO>> GetProductReviews(int productId)
        {
            var reviews = await _reviewRepository.ListAsync();
            var productReviews = reviews.Where(r => r.ProductID == productId).Select(r => new ReviewDTO
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                ProductID = r.ProductID,
                UserID = r.UserID,
                ProductName = r.Product?.Name,
                UserName = r.AppUser?.UserName
            });

            return productReviews;
        }
    }
}
