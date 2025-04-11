using Gym_Community.API.DTOs.Auth;
using Gym_Community.Domain.Models;

namespace Gym_Community.Application.Interfaces
{
    public interface IAuthService
    {
        
        public Task<string?> GenerateJwtTokenAsync(AppUser user);
        public Task<string> login(LoginDTO loginDto);

        public Task<string> register(RegisterDTO registerDTO);

        public Task<bool> IsAuthenticated(string email);

        public Task<bool> ConfirmEmail(string email, string token);
    }
}
