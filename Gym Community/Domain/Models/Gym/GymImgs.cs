using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Gym
{
    public class GymImgs
    {
        public int Id { get; set; }
        [ForeignKey("Gym")]
        public int GymId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public Gym Gym { get; set; }
    }
}
