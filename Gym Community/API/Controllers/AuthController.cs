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
        public AuthController(IAuthService authService)
        {
            _authService = authService;

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
         
            return Ok(new { message = "Account Created", token=result });
        }
    }
}
