using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyPlan
    {
        public ICollection<StaticDailyExercise> Exercises { get; set; } = new List<StaticDailyExercise>();  
        public ICollection<StaticDailyMeal> Meals { get; set; } = new List<StaticDailyMeal>();
    }
}
