using Gym_Community.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Gym
{
    public class UserSubscription
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Gym")]
        public int GymId { get; set; }
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public PaymentStatus paymentStatus { get; set; } = PaymentStatus.Pending; 
        public DateTime StartDate {  get; set; } 
        public DateTime ExpiresAt { get; set; }
        public string QrCodeData { get; set; } = null!; // store raw string or image URL

        public bool IsExpired { get; set; } = false;

        public AppUser User { get; set; }
        public Gym Gym { get; set; }

        public GymPlan Plan { get; set; }
    }
}
