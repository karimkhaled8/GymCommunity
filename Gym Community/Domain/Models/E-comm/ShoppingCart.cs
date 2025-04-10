using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class ShoppingCart
    {

        [Key]
        public int ShoppingCartID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

   
        public AppUser AppUser { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
