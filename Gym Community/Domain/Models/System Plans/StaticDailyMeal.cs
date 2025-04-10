using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyMeal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Meal")]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
        public string Name { get; set; }
        public string SuggestedTime { get; set; }
        public decimal Quantity { get; set; }  // e.g., 100 (grams), 2 (scoops)
        public string Unit { get; set; }      // e.g., "g", "scoops"

    }
}
