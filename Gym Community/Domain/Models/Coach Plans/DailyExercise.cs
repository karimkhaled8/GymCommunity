using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyExercise
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
