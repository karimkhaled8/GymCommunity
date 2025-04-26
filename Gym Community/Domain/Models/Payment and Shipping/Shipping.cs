using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Domain.Data.Models.Payment_and_Shipping
{
    public class Shipping
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int? OrderID { get; set; }
        public Order Order { get; set; }

        [MaxLength(100)]
        public string Carrier { get; set; }

        [MaxLength(50)]
        public string TrackingNumber { get; set; }
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }

        [MaxLength(50)]
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Pending;

        [Required]
        [MaxLength(255)]
        public string ShippingAddress { get; set; } = string.Empty;
    }
}
