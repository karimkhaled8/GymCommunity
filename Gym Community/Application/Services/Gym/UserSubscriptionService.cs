using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Domain.Enums;
using Gym_Community.Domain.Models.Gym;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Asn1.Pkcs;
using QRCoder;

namespace Gym_Community.Application.Services.Gym
{
    public class UserSubscriptionService:IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _repo;
        public UserSubscriptionService(IUserSubscriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserSubscriptionReadDTO?> CreateAsync(UserSubscriptionCreateDTO dto)
        {
            var rawData = Guid.NewGuid().ToString();
            Console.WriteLine("rawData: " + rawData);
            var subscription = new UserSubscription
            {
                UserId = dto.UserId,
                GymId = dto.GymId,
                PlanId = dto.PlanId,
                StartDate = dto.StartDate,
                ExpiresAt = dto.ExpiresAt,
                PurchaseDate = DateTime.UtcNow,
                rawData = rawData,
                QrCodeData = GenerateQrCodeBase64(rawData),
                paymentStatus = PaymentStatus.Pending
            };

            var result = await _repo.AddAsync(subscription);
            return result != null ? await GetByIdAsync(result.Id) : null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sub = await _repo.GetByIdAsync(id);
            if (sub == null) return false;
            return await _repo.DeleteAsync(sub);
        }

        public async Task<IEnumerable<UserSubscriptionReadDTO>> GetAllAsync()
        { 
            return (await _repo.GetAllAsync()).Select(Map);
        }

        public async Task<UserSubscriptionReadDTO?> GetByIdAsync(int id)
        {
            var sub = await _repo.GetByIdAsync(id);
            return sub == null ? null : Map(sub);
        }

        public async Task<IEnumerable<UserSubscriptionReadDTO>> GetByGymIdAsync(int gymId)
        {
            return (await _repo.GetByGymIdAsync(gymId)).Select(Map);
        }

        public async Task<IEnumerable<UserSubscriptionReadDTO>> GetByPlanIdAsync(int planId)
        {
            return (await _repo.GetByPlanIdAsync(planId)).Select(Map); 
        }

        public async Task<UserSubscriptionReadDTO?> UpdateAsync(int id, UserSubscriptionUpdateDTO dto)
        {
            var sub = await _repo.GetByIdAsync(id);
            if (sub == null) return null;

            sub.paymentStatus = dto.PaymentStatus;
            sub.StartDate = dto.StartDate;
            sub.ExpiresAt = dto.ExpiresAt;
            sub.IsExpired = dto.IsExpired;

            var updated = await _repo.UpdateAsync(sub);
            return updated != null ? await GetByIdAsync(updated.Id) : null;
        }
        public async Task<UserSubscriptionReadDTO?> ValidateQrCodeAsync(string qrCodeData)
        {
            var allSubs = await _repo.GetAllAsync();
            var sub = allSubs.FirstOrDefault(s => s.rawData == qrCodeData);

            if (sub == null || sub.IsExpired || sub.paymentStatus != PaymentStatus.Completed || sub.ExpiresAt < DateTime.UtcNow)
            {
                return null;
            }

            return Map(sub);
        }

        private string GenerateQrCodeBase64(string qrText)
        {
            using var generator = new QRCodeGenerator();
            var data = generator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var pngQrCode = new PngByteQRCode(data);
            var qrCodeBytes = pngQrCode.GetGraphic(20);

            return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
        }


        private UserSubscriptionReadDTO Map(UserSubscription sub)
        {
            return new UserSubscriptionReadDTO
            {
                Id = sub.Id,
                UserId = sub.UserId,
                GymId = sub.GymId,
                PlanId = sub.PlanId,
                PurchaseDate = sub.PurchaseDate,
                StartDate = sub.StartDate,
                ExpiresAt = sub.ExpiresAt,
                PaymentStatus = sub.paymentStatus,
                IsExpired = sub.IsExpired,
                QrCodeData = sub.QrCodeData,
                rawData = sub.rawData
            };
        }
    }
}
