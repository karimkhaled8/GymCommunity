using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateDailyPlanDto
    {
        [Required]
        public int WeekPlanId { get; set; }
        
        //public WeekPlan WeekPlan { get; set; }
        
        public string ExtraTips { get; set; }
        
        [Required]
        public DateTime DayDate { get; set; }
        
        [Required]
        public int DayNumber { get; set; }
        
        [Required]
        public string DailyPlanJson { get; set; }

        //new
        public double? TotalCalories { get; set; }
        public double? TotalProtein { get; set; }  
        public double? TotalCarbs { get; set; }
        public double? TotalFats { get; set; } 
    }

    public class UpdateDailyPlanDto
    {
        [Required]
        public int Id { get; set; }
        
        public WeekPlan WeekPlan { get; set; }
        
        public string ExtraTips { get; set; }
        
        public DateTime DayDate { get; set; }
        
        public int DayNumber { get; set; }
        
        public string DailyPlanJson { get; set; }


        //new
        public double? TotalCalories { get; set; }
        public double? TotalProtein { get; set; }
        public double? TotalCarbs { get; set; }
        public double? TotalFats { get; set; } // in grams
    }

    public class DailyPlanDto
    {
        public int Id { get; set; }
        public int WeekPlanId { get; set; }
        public WeekPlan WeekPlan { get; set; }
        public string ExtraTips { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNumber { get; set; }
        public string DailyPlanJson { get; set; }

        //new
        public double? TotalCalories { get; set; }
        public double? TotalProtein { get; set; }
        public double? TotalCarbs { get; set; }
        public double? TotalFats { get; set; } // in grams

    }
} 