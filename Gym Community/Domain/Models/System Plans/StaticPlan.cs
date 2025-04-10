using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticPlan
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int FrequencyPerWeek { get; set; }
        public string Type { get; set; }  // Cardio, Strength, Hybrid
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CaloricTarget { get; set; }
        public int ProteinPercentage { get; set; }
        public int CarbsPercentage { get; set; }
        public int FatsPercentage { get; set; }

        public ICollection<WorkoutDay> WorkoutDays { get; set; } = new List<WorkoutDay>();
    }
}
