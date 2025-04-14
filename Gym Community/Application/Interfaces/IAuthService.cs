using Gym_Community.API.DTOs.Auth;
using Gym_Community.Domain.Models;

namespace Gym_Community.Application.Interfaces
{
    public interface IAuthService
    {
        
        public Task<string?> GenerateJwtTokenAsync(AppUser user);
        public Task<string> login(LoginDTO loginDto);

        public Task<string> register(RegisterDTO registerDTO, string profileImg);

        public Task<string?> GetRole(string userID); 
        public Task<bool> IsAuthenticated(string email);

        public Task<bool> ConfirmEmail(string email, string token);

        public Task<bool> ForgotPassword(ForgetPasswordDTO forgetPasswordDTO);
        public Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO,string email,string token);
    }
}
