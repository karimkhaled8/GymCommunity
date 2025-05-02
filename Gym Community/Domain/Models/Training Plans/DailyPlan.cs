using Gym_Community.Domain.Models.Coach_Plans;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Data.Models.Meals_and_Exercise
{
    public class DailyPlan
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WeekPlan")]
        public int WeekPlanId { get; set; }
        [NotMapped]
        public WeekPlan WeekPlan { get; set; }
        public string ExtraTips { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNumber { get; set; }
        public string DailyPlanJson { get; set; }  // JSON storage for exercises and meals


        // new properties
        public double? TotalCalories { get; set; }
        public double? TotalProtein { get; set; }  // in grams
        public double? TotalCarbs { get; set; }    // in grams
        public double? TotalFats { get; set; }     // in grams

        public bool? IsDone { get; set; } = false;
    }
}
