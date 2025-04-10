using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Data;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? UserID { get; set; }
 
        public AppUser AppUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; } = "Unpaid";

        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
    }
}
