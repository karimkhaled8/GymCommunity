using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Gym_Community.API.DTOs.Coach.CoachStuff
{
    public class CoachPortfolioDto
    {
        public  int Id { get; set; }
        public string  CoachId { get; set; }

        public string? coachFirstName { get; set; }
        public string? coachLastName { get; set; }

        public string? gender { get; set; }


        public string? AboutMeImageUrl { get; set; }
        public string AboutMeDescription { get; set; }
        public string Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public string SkillsJson { get; set; } 
        public string SocialMediaLinksJson { get; set; }

        
    }
}
