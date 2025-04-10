using Gym_Community.Domain.Data.Models.E_comms;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }

        // Navigation property: list of products under this brand
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
