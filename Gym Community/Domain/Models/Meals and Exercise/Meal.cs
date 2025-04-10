namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }  // in grams
        public int Carbs { get; set; }    // in grams
        public int Fats { get; set; }     // in grams
        public bool IsSupplement { get; set; }
    }
}
