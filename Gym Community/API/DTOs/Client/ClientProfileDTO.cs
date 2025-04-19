using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.Client
{
    public class ClientProfileDTO
    {
        
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int? WorkoutAvailability { get; set; } // 3 days a week, 5 days a week, etc.


        public string ClientId { get; set; }


        public ClientGoal? clientGoal { get; set; } // Enum for client goals
        public string? OtherGoal { get; set; } // if he/she has a different goal



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string ProfileImg { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime? BirthDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPremium { get; set; } = false;

      

        public string Gender { get; set; }

        public int? bodyFat { get; set; }
        public string? Bio { get; set; }
        public string? CoverImg { get; set; }

    }
}
