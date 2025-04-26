using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateWeekPlanDto
    {
        [Required]
        public int TrainingPlanId { get; set; }
        
        //public TrainingPlan TrainingPlan { get; set; }
        
        [Required]
        public string WeekName { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
    }

    public class UpdateWeekPlanDto
    {
        [Required]
        public int Id { get; set; }
        
        public TrainingPlan TrainingPlan { get; set; }
        
        public string WeekName { get; set; }
        
        public DateTime StartDate { get; set; }
    }

    public class WeekPlanDto
    {
        public int Id { get; set; }
        public int TrainingPlanId { get; set; }
        public TrainingPlan TrainingPlan { get; set; }
        public string WeekName { get; set; }
        public DateTime StartDate { get; set; }
        public ICollection<DailyPlanDto> WorkoutDays { get; set; } = new List<DailyPlanDto>();
    }
} 