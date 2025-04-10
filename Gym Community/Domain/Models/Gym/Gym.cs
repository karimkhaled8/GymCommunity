using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Gym
{
    public class Gym
    {
        public int Id { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }

        public double Latitude { get; set; }    // 🌐 Add this
        public double Longitude { get; set; }   // 🌐 Add this

        public AppUser Owner { get; set; }
        public ICollection<GymImgs> Images { get; set; }
        public ICollection<GymCoach> Coaches { get; set; }
        public ICollection<GymPlan> GymSubscriptions { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
