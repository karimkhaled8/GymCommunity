namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string VideoLink { get; set; }
        public string Type { get; set; } // Cardio, Strength, Hybrid
    }

}
