using Gym_Community.Data;
using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class ClientPlan
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public AppUser Coach { get; set; }
        public string ClientId { get; set; }
        public AppUser Client { get; set; }
        public string Name { get; set; }
        public int DurationMonths { get; set; }  // 1, 3, or 6 months
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
