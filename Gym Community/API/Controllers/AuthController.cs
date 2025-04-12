using EmailServices;
using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IAwsService _awsService;
        public AuthController(IAuthService authService , IEmailService emailService, IAwsService awsService)
        {
            _authService = authService;
            _emailService = emailService;
            _awsService = awsService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO, [FromForm] IFormFile profileImg)
        {
            var imageUrl = "";
            if (profileImg != null)
            {
                 imageUrl = await _awsService.UploadFileAsync(profileImg, "ProfileImages");
      
            }


            var result = await _authService.register(registerDTO, imageUrl);
            if (result == "exists")
            {
                return BadRequest( new { message = "Email already exists" });
            }
            else if (result == "failed")
            {
                return BadRequest(new { message = "User creation failed" });
            }
            else if (result == "falseRole")
            {
                return BadRequest(new { message = "Role dont exist" });
            }


            return Ok(new { message = "Account Created", status=result });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) 
        {
            var result = await _authService.login(loginDTO);
            if(result == "error")
            {
                return BadRequest(new { message = "Wrong Email or Password" });
            }else if ( result== "notConfirmed") 
            {
                return BadRequest(new { message = "Email not confirmed please check your email" });
            }

           

            return Ok(new {message="login successfully",token = result});

        }


        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email,[FromQuery] string token )
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
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO, [FromQuery]string email, [FromQuery] string token)
        {
            var result = await _authService.ResetPassword(resetPasswordDTO, email, token);
            if (result =="success")
            {
                return Ok(new { message = "Password reset successfully" });
            }
            return BadRequest(new { message = "Error in resetting password" });
        }
    }
}
