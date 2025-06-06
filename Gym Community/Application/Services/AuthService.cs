﻿using EmailServices;
using Gym_Community.API.DTOs.Auth;
using Gym_Community.Application.Interfaces;
using Gym_Community.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using System.Text.Json;


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
                        new Claim("Name",$"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("ProfileImg", user.ProfileImg),
                        new Claim("IsPremium", user.IsPremium.ToString()),
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
        public async Task<string?> GetRole(string userID)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userID);
            if (user == null) return null;

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null) return null;

            return userRole.FirstOrDefault();
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

                //check for email validation
                if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        return "notConfirmed";
                    }

                if (await _userManager.CheckPasswordAsync(user, loginDto.Password)) 
                {
                    return await GenerateJwtTokenAsync(user);
                }
                return "error";
            }
            return "error";
        }

        public async Task<string> register(RegisterDTO registerDTO, string profileImg)
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
                ProfileImg = profileImg,
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
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(User);
            var param = new Dictionary<string, string?>
                {
                    { "token", confirmationToken },
                    { "email", User.Email }
                };

            var callback = QueryHelpers.AddQueryString(registerDTO.ClientUri!, param);

            await _emailService.SendEmailAsync(registerDTO.Email, "Confirm your email", $"<h1>Confirm your email</h1><p>Please confirm your email by clicking <a href='{callback}'>here</a></p>");
            //await _emailService.SendEmailAsync(registerDTO.Email, "Confirm your email", callback);
            #endregion

            //return await GenerateJwtTokenAsync(User);
            return StatusCodes.Status201Created.ToString();

        }

        public async Task<bool> ForgotPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDTO.Email);
            if (user == null)
            {
                return false;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var param = new Dictionary<string, string?>
            {
                { "token", token },
                { "email", user.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgetPasswordDTO.ClientUri!, param);
            //await _emailService.SendEmailAsync(forgetPasswordDTO.Email, "Reset your password", $"<h1>Reset your password</h1><p>Please reset your password by clicking <a href='{callback}'>here</a></p>");
            await _emailService.SendEmailAsync(forgetPasswordDTO.Email, "Reset your password", callback);

            return true;
        }

        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO, string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return "notfound";
            }
            var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordDTO.Password);
            if (result.Succeeded)
            {
                return "success";
            }
            else
            {
                return "error";
            }
        }

        public async Task<ExternalLoginInfo> GetGoogleLoginInfo(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, payload.Subject),
                new Claim(ClaimTypes.Email, payload.Email),
                new Claim(ClaimTypes.Name, payload.Name),
                new Claim(ClaimTypes.Uri, payload.Picture)
                };

                var identity = new ClaimsIdentity(claims, "Google");
                var principal = new ClaimsPrincipal(identity);

                return new ExternalLoginInfo(principal, "Google", payload.Subject, payload.Name);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ExternalLoginInfo> GetFacebookLoginInfo(string accessToken)
        {
            var verifyEndpoint = $"https://graph.facebook.com/me?access_token={accessToken}&fields=id,name,email";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(verifyEndpoint);

            var result = JsonSerializer.Deserialize<FacebookUserInfo>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, result.Id),
        new Claim(ClaimTypes.Email, result.Email ?? ""),
        new Claim(ClaimTypes.Name, result.Name ?? "")
    };

            var identity = new ClaimsIdentity(claims, "Facebook");
            var principal = new ClaimsPrincipal(identity);

            return new ExternalLoginInfo(principal, "Facebook", result.Id, result.Name);
        }

        public class FacebookUserInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

    }
}
