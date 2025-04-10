using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.System_Plans
{
    public class StaticWorkoutDay
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("StaticPlan")]
        public int StaticPlanId { get; set; }
        public StaticPlan StaticPlan { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNumber { get; set; }
        public string DailyPlanJson { get; set; }  // JSON storage for exercises and meals
    }
}
