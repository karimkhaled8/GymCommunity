namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyPlan
    {
        public ICollection<DailyExercise> Exercises { get; set; } = new List<DailyExercise>();
        public ICollection<DailyMeal> Meals { get; set; } = new List<DailyMeal>();
    }

}
