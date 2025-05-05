using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string Address { get; set; }
        //public string? ProfileImg { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        [Required]
        [RegularExpression("^(m|f|M|F)$", ErrorMessage = "Gender must be Male, Female")]
        public string Gender { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Role { get; set; }

        public string? ClientUri { get; set; } = "http://localhost:4200/confirm-done"; // For email confirmation

    }
}
