using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class WorkoutDay
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ClientPlan")]
        public int ClientPlanId { get; set; }
        public ClientPlan ClientPlan { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNumber { get; set; }
        public string DailyPlanJson { get; set; }  // JSON storage for exercises and meals
    }
}
