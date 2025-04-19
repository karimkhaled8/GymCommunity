namespace Gym_Community.API.DTOs.CoachStuff
{
    public class CoachPortfolioDto
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public string AboutMeImageUrl { get; set; }
        public string AboutMeDescription { get; set; }
        public string Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public string SkillsJson { get; set; }
        public string SocialMediaLinksJson { get; set; }
    }
}
