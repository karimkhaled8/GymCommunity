using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Gym
{
    public class GymPlan
    {
        public int Id { get; set; }
        [ForeignKey("Gym")]
        public int GymId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public bool HasPrivateCoach { get; set; } = false;
        public bool HasNutritionPlan { get; set; } = false;
        public bool HasAccessToAllAreas { get; set; } = false;
        public Gym Gym { get; set; }

        public ICollection<UserSubscription> UserSubscriptions { get; set; }
    }
}
