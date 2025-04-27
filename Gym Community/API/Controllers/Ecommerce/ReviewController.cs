using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService; 
        }

        [HttpGet("{productId}")]  // Match parameter name
        public async Task<IActionResult> Get(int productId)  // Change parameter name
        {
            var reviews = await _reviewService.GetProductReviews(productId);
            return Ok(reviews);
        }

        [HttpGet("review/{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review  = await _reviewService.GetReviewById(id);
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReviewDTO review)
        {
            var reviewId =  await _reviewService.CreateReview(review);
            if (reviewId == 0) return BadRequest("Failed to create review");
            return CreatedAtAction(nameof(GetReview), new { id = reviewId }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReviewDTO reviewDTO)
        {
            var result = await _reviewService.UpdateReview(id, reviewDTO);
            if (!result) return BadRequest("Failed to update review");
            return Ok("Review updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
                return BadRequest(new { success = false, message = "Review deleting failed!" });

            var result = await _reviewService.DeleteReview(id);

            if (result)
                return Ok(new { success = true, message = "Review deleting succeeded!" });
            else
                return BadRequest(new { success = false, message = "Review deleting failed!" });
        }

    }
}
