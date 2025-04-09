using Gym_Community.Data;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticPlan
    {
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

        public ICollection<WorkoutDay> WorkoutDays { get; set; }
    }
}
