using Gym_Community.Domain.Enums;
using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateTrainingPlanDto
    {
        
        public string? CoachId { get; set; }
        
        public string? ClientId { get; set; }
        

        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(1, 6)]
        public int DurationMonths { get; set; }
   
        
        
        public TrainingPlanType? Type { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate  => StartDate.AddMonths(DurationMonths);

        public int? CaloricTarget { get; set; }
        
        [Range(0, 100)]
        public int? ProteinPercentage { get; set; }
        
        [Range(0, 100)]
        public int? CarbsPercentage { get; set; }
        
        [Range(0, 100)]
        public int? FatsPercentage { get; set; }
    }

    public class UpdateTrainingPlanDto
    {
        [Required]
        public int Id { get; set; }
        
        public string? ClientId { get; set; }
        
        public bool IsStaticPlan { get; set; }
        
        public string Name { get; set; }
        
        [Range(1, 6)]
        public int DurationMonths { get; set; }
        
        [Range(1, 7)]
        public int FrequencyPerWeek { get; set; }
        
        public string Type { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int CaloricTarget { get; set; }
        
        [Range(0, 100)]
        public int ProteinPercentage { get; set; }
        
        [Range(0, 100)]
        public int CarbsPercentage { get; set; }
        
        [Range(0, 100)]
        public int FatsPercentage { get; set; }
    }

    public class TrainingPlanDto
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public string? ClientId { get; set; }
        public bool IsStaticPlan { get; set; }
        public string Name { get; set; }
        public int DurationMonths { get; set; }
        public int FrequencyPerWeek { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CaloricTarget { get; set; }
        public int ProteinPercentage { get; set; }
        public int CarbsPercentage { get; set; }
        public int FatsPercentage { get; set; }
        public AppUser Client { get; set; } 
        public ICollection<WeekPlanDto> WeekPlans { get; set; } = new List<WeekPlanDto>();
    }

    public class GetTrainingPlanDto
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public string? ClientId { get; set; }
        public bool IsStaticPlan { get; set; }
        public string Name { get; set; }
        public int DurationMonths { get; set; }
        public TrainingPlanType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CaloricTarget { get; set; }
        public int? ProteinPercentage { get; set; }
        public int? CarbsPercentage { get; set; }
        public int? FatsPercentage { get; set; }

        public string UserName { get; set; }
        public string ProfileImg { get; set; }
        public string Gender { get; set; }



        public ICollection<WeekPlanDto> WeekPlans { get; set; } = new List<WeekPlanDto>();
    }
} 