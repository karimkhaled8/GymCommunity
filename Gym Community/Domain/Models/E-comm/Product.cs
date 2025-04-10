using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comm;

namespace Gym_Community.Domain.Data.Models.E_comms
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        // Replaced ImageData and ImageMimeType with ImageUrl
        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public float? AverageRating { get; set; }

        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
