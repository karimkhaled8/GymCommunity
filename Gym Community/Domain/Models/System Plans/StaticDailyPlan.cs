using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyPlan
    {
        public List<StaticDailyExercise> Exercises { get; set; }
        public List<StaticDailyMeal> Meals { get; set; }
    }
}
