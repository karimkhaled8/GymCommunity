using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Models.CoachStuff
{
    public class CoachRating
    {
        public int Id { get; set; }

        public string ClientId { get; set; }
        public AppUser Client { get; set; }

        public string CoachId { get; set; }
        public AppUser Coach { get; set; }

        [Range(1, 5)]
        public int Rate { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
