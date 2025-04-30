using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.E_comm
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string? CustomerEmail{ get; set; }
        public PaymentDTO? Payment { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? UserID { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } 
        public ShippingDTO Shipping { get; set; } 

    }
}
