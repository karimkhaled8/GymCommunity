namespace Gym_Community.API.DTOs.E_comm
{
    public class ProductDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public float? AverageRating { get; set; }

        public int? CategoryID { get; set; }

        public string? CategoryName { get; set; } = string.Empty;

        public int? BrandId { get; set; }

        public string? BrandName { get; set; } = string.Empty;
    }
}
