using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("MuscleGroup")]
        public int MuscleGroupId { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string VideoLink { get; set; }
        public string Type { get; set; } // Cardio, Strength, Hybrid
    }

}
