using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyExercise
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
