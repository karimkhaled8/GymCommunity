using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Data.Models.E_comms
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
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

        [ForeignKey("User")]
        public string? OwnerId { get; set; }
        public AppUser? User { get; set; }


        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }


        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
