namespace Gym_Community.API.DTOs.Gym
{
    public class GymImgCreateDTO
    {
        public int GymId { get; set; }
        public string? ImageUrl { get; set; } = null!;
    }

    public class GymImgReadDTO
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public string ImageUrl { get; set; } = null!;
    }

}
