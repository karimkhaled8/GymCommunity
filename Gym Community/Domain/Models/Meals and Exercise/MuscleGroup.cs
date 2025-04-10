using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class MuscleGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }  // e.g., "Chest", "Back", "Legs"
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
