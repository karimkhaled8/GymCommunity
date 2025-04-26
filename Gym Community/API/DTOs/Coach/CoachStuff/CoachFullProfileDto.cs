namespace Gym_Community.API.DTOs.Coach.CoachStuff
{
    public class CoachFullProfileDto
    {
        public CoachPortfolioDto Portfolio { get; set; }
        public IEnumerable<CoachCertificateDto> Certificates { get; set; }
        public IEnumerable<WorkSampleDto > WorkSamples { get; set; }
        public IEnumerable<CoachRatingDto> Ratings { get; set; }
    }
}
