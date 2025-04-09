using Microsoft.AspNetCore.Identity;

namespace Gym_Community.Data
{
    public class AppUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string ProfileImg { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? BirthDate { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsPremium { get; set; } = false;

        public double? AvgRating { get; set; } = 0;

        public string Gender { get; set; } 





    }
}
