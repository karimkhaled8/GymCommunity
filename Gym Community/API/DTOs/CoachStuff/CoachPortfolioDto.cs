using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.CoachStuff
{
    public class CoachPortfolioDto
    {
     
        public string ? CoachId { get; set; }

        public string? AboutMeImageUrl { get; set; }
        public string AboutMeDescription { get; set; }
        public string Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public List<string> SkillsJson { get; set; } 
        public List<string> SocialMediaLinksJson { get; set; }
    }
}
