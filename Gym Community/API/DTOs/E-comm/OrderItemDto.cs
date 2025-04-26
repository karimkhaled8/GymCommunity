﻿namespace Gym_Community.API.DTOs.E_comm
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty; 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
