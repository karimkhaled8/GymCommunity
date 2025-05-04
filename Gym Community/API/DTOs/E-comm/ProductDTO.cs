using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.E_comm
{
    public class ProductDTO
    {
        [Required]
        public int Id { get; set; }  // Changed from int? to int

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public float? AverageRating { get; set; }

        public int ReviewCount { get; set; }

        [Required]
        public int? CategoryID { get; set; }  // Changed from int? to int

        public string? CategoryName { get; set; } = string.Empty;

        [Required]
        public int? BrandId { get; set; }  // Changed from int? to int

        public string? BrandName { get; set; } = string.Empty;
    }
}