namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyMeal
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public string SuggestedTime { get; set; }
        public decimal Quantity { get; set; }  // e.g., 100 (grams), 2 (scoops)
        public string Unit { get; set; }      // e.g., "g", "scoops"
    }
}
