﻿using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.Auth
{
    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string? ClientUri { get; set; } = "https://www.ourWebsite.com/ResetPassword"; 
    }
}
