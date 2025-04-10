using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyExercise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
