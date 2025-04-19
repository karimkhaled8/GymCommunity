using Gym_Community.Domain.Models.ClientStuff;

namespace Gym_Community.Infrastructure.Interfaces.Client
{
    public interface IClientInfoRepository
    {
        public Task AddClientInfoAsync(ClientInfo clientInfo);
        public Task<ClientInfo?> GetClientInfoByUserIdAsync(string userId);
        public Task UpdateClientInfoAsync(ClientInfo clientInfo);
        public Task DeleteClientInfoAsync(string userId);

    }
}
