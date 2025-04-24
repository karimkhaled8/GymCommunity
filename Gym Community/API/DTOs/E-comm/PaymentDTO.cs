using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.E_comm
{
    public class PaymentDTO
    {
        public int? Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;

    }
}
