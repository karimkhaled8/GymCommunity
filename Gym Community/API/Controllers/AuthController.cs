using EmailServices;
using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
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
        public AuthController(IAuthService authService , IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;

        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            // prpfile image logic

            
            var result = await _authService.register(registerDTO);
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

            return Ok(new { message = "Account Created", token=result });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) 
        {
            var result = await _authService.login(loginDTO);
            if(result == "error")
            {
                return BadRequest(new { message = "Wrong Email or Password" });
            }

            await _emailService.SendEmailAsync("mostafasamir112002@gmail.com", "testmail", "hello form gym");

            return Ok(new {message="login successfully",token = result});

        }
    }
}
