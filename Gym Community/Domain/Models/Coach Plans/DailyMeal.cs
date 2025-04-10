using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyMeal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SuggestedTime { get; set; }
        public decimal Quantity { get; set; }  // e.g., 100 (grams), 2 (scoops)
        public string Unit { get; set; }      // e.g., "g", "scoops"
    }
}
