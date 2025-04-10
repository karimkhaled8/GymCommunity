using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.E_comms;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class ShoppingCartItem
    {
        [Required]
        [Key]
        public int ShoppingCartItemID { get; set; }

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartID { get; set; } 
        [ForeignKey("Product")]
        public int ProductID { get; set; } 

        public int Quantity { get; set; }

       
        public ShoppingCart? ShoppingCart { get; set; }

      
        public Product? Product { get; set; }
    }
}
