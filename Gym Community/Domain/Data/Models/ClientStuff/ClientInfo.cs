using Gym_Community.Domain.Data.Enums;

namespace Gym_Community.Domain.Data.Models.ClientStuff
{
    public class ClientInfo
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public int WorkoutAvailability { get; set; } // 3 days a week, 5 days a week, etc.

        public ClientGoal clientGoal { get; set; } // Enum for client goals
        public string? OtherGoal { get; set; } // if he/she has a different goal
    }
}
