﻿using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Domain.Models;
using Microsoft.AspNetCore.Identity;
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

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;

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

        public Task<string> login(LoginDTO loginDto)
        {
            throw new NotImplementedException();
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
            if (!string.IsNullOrEmpty(registerDTO.Role))
            {
                await _userManager.AddToRoleAsync(User, registerDTO.Role);
            }
            return await GenerateJwtTokenAsync(User);
          
        }
    }
}
