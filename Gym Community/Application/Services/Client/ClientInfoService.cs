using Gym_Community.API.DTOs.Client;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Infrastructure.Interfaces.Client;
using Gym_Community.Infrastructure.Repositories.Client;

namespace Gym_Community.Application.Services.Client
{
    public class ClientInfoService : IClientInfoService
    {
        private readonly IClientInfoRepository _clientInfoRepository;
        public ClientInfoService(IClientInfoRepository clientInfoRepository)
        {
            _clientInfoRepository = clientInfoRepository;
        }
        public async Task<bool> AddClientInfoAsync(ClientInfoDTO clientInfo)
        {
            if (clientInfo == null)
            {
                var newClientInfo = new Domain.Models.ClientStuff.ClientInfo
                {
                    Height = clientInfo.Height,
                    Weight = clientInfo.Weight,
                    WorkoutAvailability = clientInfo.WorkoutAvailability,
                    clientGoal = clientInfo.clientGoal,
                    OtherGoal = clientInfo.OtherGoal,
                    Client = clientInfo.ClientId,
                    Bio = clientInfo.Bio,
                    bodyFat = clientInfo.bodyFat

                };
                await _clientInfoRepository.AddClientInfoAsync(newClientInfo);
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeCoverImg(string img, string userId)
        {
            var clientInfo = await _clientInfoRepository.GetClientInfoByUserIdAsync(userId);
            if (clientInfo != null)
            {
                clientInfo.CoverImg = img;
                await _clientInfoRepository.UpdateClientInfoAsync(clientInfo);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteClientInfoAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _clientInfoRepository.DeleteClientInfoAsync(userId);
                return true;
            }
            return false;
        }

        public async Task<ClientInfoDTO?> GetClientInfoByUserIdAsync(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var info =  await _clientInfoRepository.GetClientInfoByUserIdAsync(userId);
                if (info != null)
                {
                    return new ClientInfoDTO
                    {
                        Id = info.Id,
                        Height = info.Height,
                        Weight = info.Weight,
                        WorkoutAvailability = info.WorkoutAvailability,
                        clientGoal = info.clientGoal,
                        OtherGoal = info.OtherGoal,
                        ClientId = info.Client,
                        Bio = info.Bio,
                        bodyFat = info.bodyFat,


                    };
                }
                return null;
            }
            return null;
        }

        public async Task<bool> UpdateClientInfoAsync(string id, ClientInfoDTO clientInfo)
        {
            var updateInfo = await _clientInfoRepository.GetClientInfoByUserIdAsync(id);
            if (updateInfo != null)
            {
                updateInfo.Height = clientInfo.Height;
                updateInfo.Weight = clientInfo.Weight;
                updateInfo.WorkoutAvailability = clientInfo.WorkoutAvailability;
                updateInfo.clientGoal = clientInfo.clientGoal;
                updateInfo.OtherGoal = clientInfo.OtherGoal;
                updateInfo.Bio = clientInfo.Bio;
                updateInfo.bodyFat = clientInfo.bodyFat;
                await _clientInfoRepository.UpdateClientInfoAsync(updateInfo);
                return true;
            }
            return false;
        }
    }
}
