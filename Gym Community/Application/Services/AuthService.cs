using EmailServices;
using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gym_Community.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<bool> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            var confirmed = await _userManager.ConfirmEmailAsync(user, token);
            if (confirmed.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<string?> GenerateJwtTokenAsync(AppUser user)
        {
            if (user != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("ProfileImg", user.ProfileImg),
                    };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(3),
                    signingCredentials: credentials
                );
                
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> IsAuthenticated(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<string> login(LoginDTO loginDto)
        {
            
            if(await IsAuthenticated(loginDto.Email))
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                // check for email validation 
                //if(!await _userManager.IsEmailConfirmedAsync(user)) { 
                //    return "notConfirmed";
                //}

                if (await _userManager.CheckPasswordAsync(user, loginDto.Password)) 
                {
                    return await GenerateJwtTokenAsync(user);
                }
                return "error";
            }
            return "error";
        }

        public async Task<string> register(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return string.Empty; 
            }
            if (await IsAuthenticated(registerDTO.Email))
            {
                return "exists";
            }
            var User = new AppUser
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Address = registerDTO.Address,
                ProfileImg = registerDTO.ProfileImg,
                Gender = registerDTO.Gender,
                PhoneNumber = registerDTO.Phone,
                BirthDate = registerDTO.BirthDate,
                UserName = registerDTO.Email,
                


            };
            var result = await _userManager.CreateAsync(User, registerDTO.Password);
            if (!result.Succeeded)
            {
                return "failed"; 
            }
            var roleExist = await _roleManager.RoleExistsAsync(registerDTO.Role);
            if (!roleExist)
            {
                await _userManager.DeleteAsync(User);
                return "falseRole";
            }
                await _userManager.AddToRoleAsync(User, registerDTO.Role);

            #region Email confirmation pls dont delete
            //var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(User);
            //var param = new Dictionary<string, string?>
            //    {
            //        { "token", confirmationToken },
            //        { "email", User.Email }
            //    };

            //var callback =QueryHelpers.AddQueryString(registerDTO.ClientUri!, param);

            //await _emailService.SendEmailAsync(registerDTO.Email, "Confirm your email", $"<h1>Confirm your email</h1><p>Please confirm your email by clicking <a href='{callback}'>here</a></p>");
            //await _emailService.SendEmailAsync(registerDTO.Email, "Confirm your email", callback);
            #endregion

            //return await GenerateJwtTokenAsync(User);
            return StatusCodes.Status201Created.ToString();

        }
    }
}
