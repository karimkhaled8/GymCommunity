using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyExercise
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
