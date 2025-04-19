using Gym_Community.API.DTOs.Client;

namespace Gym_Community.Application.Interfaces.Client
{
    public interface IClientInfoService
    {
        Task<bool> AddClientInfoAsync(ClientInfoDTO clientInfo);
        Task<ClientInfoDTO?> GetClientInfoByUserIdAsync(string userId);
        Task<bool> UpdateClientInfoAsync(string id, ClientInfoDTO clientInfo);
        Task<bool> DeleteClientInfoAsync(string userId);

        Task<bool> ChangeCoverImg(string img, string userId);
    }
}
