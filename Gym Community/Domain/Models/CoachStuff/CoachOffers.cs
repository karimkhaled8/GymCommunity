using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.CoachStuff
{
    public class CoachOffers
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Desc { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public int DurationMonths { get; set; }
        [ForeignKey("Coach")]
        public string CoachId { get; set; }
        public AppUser Coach { get; set; }
    }
}
