﻿using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.Auth
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ProfileImg { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? Role { get; set; }

        public string? ClientUri { get; set; } = "https://www.ourWebsite.com/api/auth/ConfirmEmail"; // For email confirmation

    }
}
