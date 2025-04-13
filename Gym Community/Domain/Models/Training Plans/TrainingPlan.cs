using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.Coach_Plans;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class TrainingPlan
    {
        [Key]
        public int Id { get; set; }
        

        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public AppUser Coach { get; set; }
        public string? ClientId { get; set; }
        public bool IsStaticPlan { get; set; }
        public string Name { get; set; }
        public int DurationMonths { get; set; }  // 1, 3, or 6 months
        public int FrequencyPerWeek { get; set; }
        public string Type { get; set; }  //Convert into Enum Cardio, Strength, Hybrid
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CaloricTarget { get; set; }
        public int ProteinPercentage { get; set; }
        public int CarbsPercentage { get; set; }
        public int FatsPercentage { get; set; }

        public ICollection<WeekPlan> WeekPlans { get; set; } = new List<WeekPlan>();
    }
}
