using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comms;
using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public Product? Product { get; set; }


        [ForeignKey("AppUser")]
        public string? UserID { get; set; }
        public AppUser? AppUser { get; set; }


        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
