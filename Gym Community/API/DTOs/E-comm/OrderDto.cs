using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.E_comm
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? CustomerId { get; set; } = string.Empty;
        public PaymentStatus PaymentStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public ShippingStatus? ShippingStatus { get; set; }
        public DateTime? DelivaryDate { get; set; }
    }
}
