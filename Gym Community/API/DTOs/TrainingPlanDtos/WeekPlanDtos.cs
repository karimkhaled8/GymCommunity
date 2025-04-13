using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateWeekPlanDto
    {
        [Required]
        public int TrainingPlanId { get; set; }
        
        [Required]
        public string WeekName { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
    }

    public class UpdateWeekPlanDto
    {
        [Required]
        public int Id { get; set; }
        
        public string WeekName { get; set; }
        
        public DateTime StartDate { get; set; }
    }

    public class WeekPlanDto
    {
        public int Id { get; set; }
        public int TrainingPlanId { get; set; }
        public string WeekName { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<DailyPlanDto> WorkoutDays { get; set; } = new List<DailyPlanDto>();
    }
} 