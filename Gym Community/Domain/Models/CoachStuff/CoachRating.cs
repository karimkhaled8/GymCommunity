using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.CoachStuff
{
    public class CoachRating
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public AppUser Client { get; set; }
       
        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public AppUser Coach { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
