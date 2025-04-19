using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.CoachStuff
{
    public class CoachPortfolio
    {
        
        [Key]
        public int Id { get; set; }

        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public AppUser Coach { get; set; }

        public string AboutMeImageUrl { get; set; } // URL of the image that represents the coach   
        public string AboutMeDescription { get; set; } // Brief description of the coach and what they offer


        public string Qualifications { get; set; } // Certifications or degrees
        public int ExperienceYears { get; set; } 
        public List<string> SkillsJson { get; set; } 
        public List<string> SocialMediaLinksJson { get; set; }

    }
}
