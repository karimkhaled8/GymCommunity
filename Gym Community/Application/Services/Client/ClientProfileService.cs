using AutoMapper;
using Gym_Community.API.DTOs.Client;
using Gym_Community.Application.Interfaces.Client;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.ClientStuff;
using Gym_Community.Infrastructure.Interfaces.Client;
using Microsoft.AspNetCore.Identity;

namespace Gym_Community.Application.Services.Client
{
    public class ClientProfileService : IClientProfileService
    {
        private readonly IClientInfoRepository _clientInfoRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ClientProfileService(IClientInfoRepository clientInfoRepo , IMapper mapper, UserManager<AppUser> userManager)
        {
            _clientInfoRepo = clientInfoRepo;
            _mapper = mapper;
            _userManager = userManager;

        }

        public async Task<ClientProfileDTO?> GetClientProfileByUserIdAsync(string userId)
        {
            
            var clientInfoFromDb = await _clientInfoRepo.GetClientInfoByUserIdAsync(userId.Trim());
            var Client = await _userManager.FindByIdAsync(userId.Trim());
            
            if (clientInfoFromDb != null)
            {
                var clientProfile = new ClientProfileDTO 
                {
                   
                    Height = clientInfoFromDb.Height,
                    Weight = clientInfoFromDb.Weight,
                    WorkoutAvailability = clientInfoFromDb.WorkoutAvailability,
                    clientGoal = clientInfoFromDb.clientGoal,
                    OtherGoal = clientInfoFromDb.OtherGoal,
                    ClientId = clientInfoFromDb.Client,
                    FirstName = clientInfoFromDb.ClientUser.FirstName,
                    LastName = clientInfoFromDb.ClientUser.LastName,
                    Address = clientInfoFromDb.ClientUser.Address,
                    ProfileImg = clientInfoFromDb.ClientUser.ProfileImg,
                    CreatedAt = clientInfoFromDb.ClientUser.CreatedAt,
                    BirthDate = clientInfoFromDb.ClientUser.BirthDate,
                    IsActive = clientInfoFromDb.ClientUser.IsActive,
                    IsPremium= clientInfoFromDb.ClientUser.IsPremium,
                    Gender = clientInfoFromDb.ClientUser.Gender,
                    bodyFat = clientInfoFromDb.bodyFat,
                    Bio = clientInfoFromDb.Bio,
                    CoverImg = clientInfoFromDb.CoverImg


                };
                return clientProfile;
            }
            else if(Client != null)
            {
                var clientProfile = _mapper.Map<ClientProfileDTO>(Client);
                clientProfile.ClientId = Client.Id;
                return clientProfile;
            }
            else
            {
                return null;
            }

        }




        public async Task<bool> UpdateClientProfileAsync(UpdateClientProfileDTO clientInfoDTO, string userId)
        {
            var clientInfoFromDb = await _clientInfoRepo.GetClientInfoByUserIdAsync(userId);

            var userFromDb = await _userManager.FindByIdAsync(userId);
            if (userFromDb == null)
            {
                return false;
            }
            if (clientInfoFromDb != null )
            {
                _mapper.Map(clientInfoDTO, clientInfoFromDb);
                _mapper.Map(clientInfoDTO, userFromDb);
                await _userManager.UpdateAsync(userFromDb);
                await _clientInfoRepo.UpdateClientInfoAsync(clientInfoFromDb);
                return true;


            }
            else
            {
                var newClientInfo = new ClientInfo
                {
                    Height = clientInfoDTO.Height,
                    Weight = clientInfoDTO.Weight,
                    WorkoutAvailability = clientInfoDTO.WorkoutAvailability,
                    clientGoal = clientInfoDTO.clientGoal,
                    OtherGoal = clientInfoDTO.OtherGoal,
                    Client = userFromDb.Id
                };
                await _clientInfoRepo.AddClientInfoAsync(newClientInfo);
                return true;
            }
    

        }
    }
}
