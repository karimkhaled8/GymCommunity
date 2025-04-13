using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.TrainingPlanDtos
{
    public class CreateTrainingPlanDto
    {
        [Required]
        public string CoachId { get; set; }
        
        public string? ClientId { get; set; }
        
        [Required]
        public bool IsStaticPlan { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(1, 6)]
        public int DurationMonths { get; set; }
        
        [Required]
        [Range(1, 7)]
        public int FrequencyPerWeek { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [Required]
        public int CaloricTarget { get; set; }
        
        [Required]
        [Range(0, 100)]
        public int ProteinPercentage { get; set; }
        
        [Required]
        [Range(0, 100)]
        public int CarbsPercentage { get; set; }
        
        [Required]
        [Range(0, 100)]
        public int FatsPercentage { get; set; }
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
        public ICollection<WeekPlanDto> WeekPlans { get; set; } = new List<WeekPlanDto>();
    }
} 