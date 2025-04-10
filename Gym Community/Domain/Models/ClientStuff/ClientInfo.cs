using Gym_Community.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.ClientStuff
{
    public class ClientInfo
    {
        [Key]
        public int Id { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int WorkoutAvailability { get; set; } // 3 days a week, 5 days a week, etc.

        [ForeignKey("ClientUser")]
        public string Client { get; set; }
        public AppUser ClientUser { get; set; }

        public ClientGoal clientGoal { get; set; } // Enum for client goals
        public string? OtherGoal { get; set; } // if he/she has a different goal
    }
}
