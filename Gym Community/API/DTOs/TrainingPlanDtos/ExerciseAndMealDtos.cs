using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MuscleGroupId { get; set; }
        public string MuscleGroupName { get; set; }
        public string VideoLink { get; set; }
        public string Type { get; set; }
    }

    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public bool IsSupplement { get; set; }
    }
} 