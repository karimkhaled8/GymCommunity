namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticDailyExercise
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
