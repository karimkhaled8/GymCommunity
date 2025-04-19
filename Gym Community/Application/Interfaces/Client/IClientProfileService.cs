using Gym_Community.API.DTOs.Client;

namespace Gym_Community.Application.Interfaces.Client
{
    public interface IClientProfileService
    {
        Task<ClientProfileDTO?> GetClientProfileByUserIdAsync(string userId);
        
        Task<bool> UpdateClientProfileAsync(UpdateClientProfileDTO clientInfoDTO, string userId);

    }
}
