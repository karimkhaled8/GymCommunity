using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.Gym
{
    public class UserSubscriptionCreateDTO
    {
        public string UserId { get; set; } = null!;
        public int GymId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiresAt { get; set; }

        public PaymentStatus paymentStatus { get; set; }
    }
    public class UserSubscriptionUpdateDTO
    {
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired { get; set; }
    }

    public class UserSubscriptionReadDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int GymId { get; set; }
        public string GymName { get; set; } = string.Empty;
        public int PlanId { get; set; }
        public string PlanTitle { get; set; } = string.Empty;
        public int PlanDuration { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public DateTime PurchaseDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsExpired { get; set; }
        public string QrCodeData { get; set; } = null!;
        public string rawData { get; set; } = null!;
    }
}
