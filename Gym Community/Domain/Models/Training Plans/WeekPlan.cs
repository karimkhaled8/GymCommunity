using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Models.Coach_Plans
{
    public class WeekPlan
    {

        [Key]
        public int Id { get; set; }
        [ForeignKey("ClientPlan")]
        public int TrainingPlanId { get; set; }
        [NotMapped]
        public TrainingPlan TrainingPlan { get; set; }

        public string WeekName { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<DailyPlan> WorkoutDays { get; set; } = new List<DailyPlan>();


    }
}
