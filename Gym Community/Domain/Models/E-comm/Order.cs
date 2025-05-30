﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Domain.Data.Models.E_comm
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("AppUser")]
        public string? UserID { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } 

        public Shipping? Shipping { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
