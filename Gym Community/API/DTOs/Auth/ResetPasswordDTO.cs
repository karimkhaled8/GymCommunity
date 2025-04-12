using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.Auth
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage ="Password is Required")]
        public string? Password { get; set; }
        [Compare("Password",ErrorMessage ="The password and confiramtion password do not match.") ]
        public string? RePassword { get; set; }

        //public string? Token { get; set; } 
        //public string? Email { get; set; } 
    }
}
