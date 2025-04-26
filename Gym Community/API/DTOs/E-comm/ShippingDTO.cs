using Gym_Community.Domain.Enums;

namespace Gym_Community.API.DTOs.E_comm
{
    public class ShippingDTO
    {
        public int? OrderID { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
    }
}
