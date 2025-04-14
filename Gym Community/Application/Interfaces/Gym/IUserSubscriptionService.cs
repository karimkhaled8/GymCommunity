﻿using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IUserSubscriptionService
    {
        Task<IEnumerable<UserSubscriptionReadDTO>> GetAllAsync();
        Task<UserSubscriptionReadDTO?> GetByIdAsync(int id);
        Task<IEnumerable<UserSubscriptionReadDTO>> GetByGymIdAsync(int gymId);
        Task<IEnumerable<UserSubscriptionReadDTO>> GetByPlanIdAsync(int planId);
        Task<UserSubscriptionReadDTO> CreateAsync(UserSubscriptionCreateDTO dto);
        Task<UserSubscriptionReadDTO?> UpdateAsync(UserSubscriptionUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<UserSubscriptionReadDTO?> ValidateQrCodeAsync(string qrCodeData);

    }
}
