using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Domain.Data.Models.Payment_and_Shipping
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public string Currency { get; set; } = string.Empty; 

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [MaxLength(100)]
        public string? StripePaymentIntentId { get; set; } 

        [MaxLength(100)]
        public string? StripeCustomerId { get; set; } 

        [MaxLength(100)]
        public string? StripeChargeId { get; set; }
    }
}
