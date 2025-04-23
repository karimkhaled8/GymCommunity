using EmailServices;
using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Services;
using Gym_Community.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IAwsService _awsService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService, IEmailService emailService, IAwsService awsService , UserManager<AppUser> userManager)
        {
            _authService = authService;
            _emailService = emailService;
            _awsService = awsService;
            _userManager = userManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO, [FromForm] IFormFile profileImg)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string imageUrl = string.Empty;
            if (profileImg != null)
            {
                imageUrl = await _awsService.UploadFileAsync(profileImg, "ProfileImages");

            }


            var result = await _authService.register(registerDTO, imageUrl);
            if (result == "exists")
            {
                return BadRequest(new { message = "Email already exists" });
            }
            else if (result == "failed")
            {
                return BadRequest(new { message = "User creation failed" });
            }
            else if (result == "falseRole")
            {
                return BadRequest(new { message = "Role dont exist" });
            }


            return Ok(new { message = "Account Created", status = result });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _authService.login(loginDTO);
            if (result == "error")
            {
                return BadRequest(new { message = "Wrong Email or Password" });
            }
            else if (result == "notConfirmed")
            {
                return BadRequest(new { message = "Email not confirmed please check your email" });
            }



            return Ok(new { message = "login successfully", token = result });

        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var result = await _authService.ConfirmEmail(email, token);
            if (result)
            {
                return Ok(new { message = "Email confirmed" });
            }
            return BadRequest(new { message = "Error in email confirmation" });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDTO forgetPasswordDTO)
        {
            var result = await _authService.ForgotPassword(forgetPasswordDTO);
            if (result)
            {
                return Ok(new { message = "Please check your email" });
            }
            return BadRequest(new { message = "Error in sending email" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO, [FromQuery] string email, [FromQuery] string token)
        {
            var result = await _authService.ResetPassword(resetPasswordDTO, email, token);
            if (result == "success")
            {
                return Ok(new { message = "Password reset successfully" });
            }
            return BadRequest(new { message = "Error in resetting password" });
        }

        [HttpPost("externallogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDTO externalLoginDTO)
        {
            ExternalLoginInfo info = null;
            if (externalLoginDTO.Provider == "Google")
            {
                info = await _authService.GetGoogleLoginInfo(externalLoginDTO.IdToken);
            }
            else if (externalLoginDTO.Provider == "Facebook")
            {
                info = await _authService.GetFacebookLoginInfo(externalLoginDTO.IdToken);
            }
            if (info == null) return BadRequest("Invalid external login info");

            var user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));

            var isNewUser = false;
            if (user == null)
            {
                // Cast or map IdentityUser to AppUser
                var appUser = new AppUser
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Address = info.Principal.FindFirstValue(ClaimTypes.StreetAddress)??"",
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "",
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? "",
                    Gender ="m",
                    ProfileImg = info.Principal.FindFirstValue("picture") ?? "",

                };

                var result = await _userManager.CreateAsync(appUser);
                if (!result.Succeeded) return BadRequest("User creation failed");

                await _userManager.AddLoginAsync(appUser, info);
                user = appUser;

                isNewUser = true;
            }

            var token = await _authService.GenerateJwtTokenAsync(user); 
            return Ok(new { message = "Login successful", Token= token, IsNewUser = isNewUser });
        }


        [HttpPost("set-role")]
        //[Authorize] // make sure only authenticated users can hit this
        public async Task<IActionResult> SetRole([FromBody] string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return BadRequest("Role cannot be empty.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            if (await _userManager.IsInRoleAsync(user, role))
                return BadRequest("User already has this role.");

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                return StatusCode(500, "Failed to assign role.");

            var token = await _authService.GenerateJwtTokenAsync(user);

            return Ok(new {message= "Role assigned successfully." ,Token=token});
        }


    }
}
