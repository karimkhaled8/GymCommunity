using Gym_Community.Domain.Data.Models.E_comms;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public ICollection<Product>? Products { get; set; }= new List<Product>();
    }
}
