using Microsoft.AspNetCore.Identity;

namespace Gym_Community.Data
{
    public class AppUser :IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
            
        public string Address { get; set; }

       

    }
}
