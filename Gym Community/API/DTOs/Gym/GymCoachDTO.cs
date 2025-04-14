namespace Gym_Community.API.DTOs.Gym
{
    public class GymCoachDTO
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public string CoachID { get; set; } = string.Empty;
        public string? CoachName { get; set; }
        public string? GymName { get; set; }

    }
    public class GymCoachCreateDTO
    {
        public int GymId { get; set; }
        public string CoachID { get; set; } = string.Empty;
    }
}
