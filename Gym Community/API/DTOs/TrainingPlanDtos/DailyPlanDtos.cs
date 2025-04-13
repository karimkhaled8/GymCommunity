using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateDailyPlanDto
    {
        [Required]
        public int WeekPlanId { get; set; }
        
        public string ExtraTips { get; set; }
        
        [Required]
        public DateTime DayDate { get; set; }
        
        [Required]
        public int DayNumber { get; set; }
        
        [Required]
        public string DailyPlanJson { get; set; }
    }

    public class UpdateDailyPlanDto
    {
        [Required]
        public int Id { get; set; }
        
        public string ExtraTips { get; set; }
        
        public DateTime DayDate { get; set; }
        
        public int DayNumber { get; set; }
        
        public string DailyPlanJson { get; set; }
    }

    public class DailyPlanDto
    {
        public int Id { get; set; }
        public int WeekPlanId { get; set; }
        public string ExtraTips { get; set; }
        public DateTime DayDate { get; set; }
        public int DayNumber { get; set; }
        public string DailyPlanJson { get; set; }
    }
} 