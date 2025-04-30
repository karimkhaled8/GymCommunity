namespace Gym_Community.API.DTOs.Coach.CoachStuff
{
    public class CoachOfferDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Desc { get; set; }
        public double Price { get; set; }
        public string?   ImageUrl { get; set; }
        public int DurationMonths { get; set; }
        public string CoachId { get; set; }
        public string? CoachName { get; set; } 
    }


    public class CreateCoachOfferDto
    {
        public string Title { get; set; }
        public string? Desc { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public int DurationMonths { get; set; }
        public string? CoachId { get; set; }
    }
}
