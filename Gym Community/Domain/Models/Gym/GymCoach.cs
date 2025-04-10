using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Gym
{
    public class GymCoach
    {
        public int Id { get; set; }
        [ForeignKey("Gym")]
        public int GymId { get; set; }
        [ForeignKey("Coach")]
        public string CoachID { get; set; }
        public AppUser Coach { get; set; }
        public Gym Gym { get; set; }
    }
}
