using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Application.Services.E_comm
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto?> CreateOrderAsync(OrderDto orderDto)
        {
            // Manually map DTO to entity model
            var order = new Order
            {
                UserID = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                PaymentStatus = orderDto.PaymentStatus,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductID = oi.ProductId,  // Use ProductId
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            var createdOrder = await _orderRepository.AddAsync(order);
            if (createdOrder == null)
            {
                return null;
            }

            // Manually map entity back to DTO for response
            return new OrderDto
            {
                Id = createdOrder.OrderID,
                OrderDate = createdOrder.OrderDate,
                TotalAmount = createdOrder.TotalAmount,
                CustomerId = createdOrder.UserID,
                PaymentStatus = createdOrder.PaymentStatus,
                OrderItems = createdOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty  // Ensure Product is correctly mapped
                }).ToList(),
                ShippingStatus = null,  // Set based on your business logic
                DelivaryDate = null     // Set based on your business logic, e.g., from Shipping
            };
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var orders = await _orderRepository.ListAsync();
            // Manually map entity to DTO
            return orders.Select(order => new OrderDto
            {
                Id = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.UserID,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty // Ensure Product is correctly mapped
                }).ToList(),
                ShippingStatus = null,  // Set based on your business logic
                DelivaryDate = null     // Set based on your business logic, e.g., from Shipping
            }).ToList();
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrderAsync(string userId)
        {
            var orders = await _orderRepository.ListUserOrdersAsync(userId);
            
            return orders.Select(order => new OrderDto
            {
                Id = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.UserID,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty 
                }).ToList(),
                ShippingStatus = order.Shipping.ShippingStatus,  
                DelivaryDate = order.Shipping.EstimatedDeliveryDate
            }).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
            {
                return null;
            }

            // Manually map entity to DTO
            return new OrderDto
            {
                Id = order.OrderID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.UserID,
                PaymentStatus = order.PaymentStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty // Ensure Product is correctly mapped
                }).ToList(),
                ShippingStatus = null,  // Set based on your business logic
                DelivaryDate = null     // Set based on your business logic, e.g., from Shipping
            };
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, OrderDto orderDto)
        {
            var existingOrder = await _orderRepository.GetById(id);
            if (existingOrder == null)
            {
                return null;
            }

            // Update the order with the new details from the DTO
            existingOrder.UserID = orderDto.CustomerId;
            existingOrder.OrderDate = orderDto.OrderDate;
            existingOrder.TotalAmount = orderDto.TotalAmount;
            existingOrder.PaymentStatus = orderDto.PaymentStatus;
            existingOrder.OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
            {
                ProductID = oi.ProductId,  // Use ProductId
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList();

            var updatedOrder = await _orderRepository.UpdateAsync(existingOrder);
            if (updatedOrder == null)
            {
                return null;
            }

            // Manually map entity back to DTO for response
            return new OrderDto
            {
                Id = updatedOrder.OrderID,
                OrderDate = updatedOrder.OrderDate,
                TotalAmount = updatedOrder.TotalAmount,
                CustomerId = updatedOrder.UserID,
                PaymentStatus = updatedOrder.PaymentStatus,
                OrderItems = updatedOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty // Ensure Product is correctly mapped
                }).ToList(),
                ShippingStatus = null,  // Set based on your business logic
                DelivaryDate = null     // Set based on your business logic, e.g., from Shipping
            };
        }

        public async Task<bool> RemoveOrderAsync(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
            {
                return false;
            }

            return await _orderRepository.RemoveAsync(order);
        }
    }
}
