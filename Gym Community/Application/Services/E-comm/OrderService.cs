using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Gym_Community.Domain.Enums;
using Gym_Community.Infrastructure.Repositories.ECommerce;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.API.DTOs;

namespace Gym_Community.Application.Services.E_comm
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingRepository _shippingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public OrderService(IOrderRepository orderRepository
            , IShippingRepository shippingRepository
            ,IPaymentRepository paymentRepository)
        {
            _orderRepository = orderRepository;
            _shippingRepository = shippingRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<OrderDto?> CreateOrderAsync(OrderDto orderDto, string userId)
        {
            var payment = await _paymentRepository.GetById(orderDto.PaymentId);
            if (payment == null)
            {
                return null;
            }
            var order = new Order
            {
                UserID = userId,
                OrderDate = DateTime.UtcNow,
                PaymentId= orderDto.PaymentId,
                OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductID = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

            var createdOrder = await _orderRepository.AddAsync(order);
            if (createdOrder == null)
            {
                return null;
            }
            var shipping = new Shipping
            {
                OrderID = createdOrder.OrderID,
                Carrier = orderDto.Shipping.Carrier,
                TrackingNumber = orderDto.Shipping.TrackingNumber,
                Latitude = orderDto.Shipping.Latitude,
                Longitude = orderDto.Shipping.Longitude,
                CustomerName = orderDto.Shipping.CustomerName,
                PhoneNumber = orderDto.Shipping.PhoneNumber,
                EstimatedDeliveryDate = orderDto.Shipping.EstimatedDeliveryDate,
                ShippingStatus = ShippingStatus.Pending,
                ShippingAddress = orderDto.Shipping.ShippingAddress
            };

            var createdShipping = await _shippingRepository.AddAsync(shipping);
             
            return new OrderDto
            {
                Id = createdOrder.OrderID,
                UserID = createdOrder.UserID,
                PaymentId = createdOrder.PaymentId,
                OrderItems = createdOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = order.OrderID,
                    ProductId = oi.ProductID,
                    ProductName = oi.Product?.Name ?? string.Empty,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                }).ToList(),
                Shipping = new ShippingDTO
                {
                    OrderID = createdShipping.OrderID,
                    Carrier = createdShipping.Carrier,
                    CustomerName = createdShipping.CustomerName,
                    PhoneNumber = createdShipping.PhoneNumber,
                    TrackingNumber = createdShipping.TrackingNumber,
                    Latitude = createdShipping.Latitude,
                    Longitude = createdShipping.Longitude,
                    EstimatedDeliveryDate = createdShipping.EstimatedDeliveryDate,
                    ShippingStatus = createdShipping.ShippingStatus,
                    ShippingAddress = createdShipping.ShippingAddress
                }
            };
        }

        public async Task<PageResult<OrderDto>> GetOrdersAsync(string query, int page, int eleNo, string sort, ShippingStatus? status, DateOnly? date)
        {
            var pagedOrders = await _orderRepository.ListAsync(query, page, eleNo, sort, status, date);

            var orderDtos = pagedOrders.Items.Select(order => new OrderDto
            {
                Id = order.OrderID,
                UserID = order.UserID,
                CustomerEmail = order.AppUser.UserName,
                Payment = new PaymentDTO
                {
                    Id = order.PaymentId,
                    Status = order.Payment.Status,
                    Amount= order.Payment.Amount,
                    Currency= order.Payment.Currency,
                    PaymentMethod= order.Payment.PaymentMethod,
                    CreatedAt = order.Payment.CreatedAt,    
                },
                PaymentId = order.PaymentId,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = order.OrderID,
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty
                }).ToList(),
                Shipping = new ShippingDTO
                {
                    Id = order.Shipping.Id,
                    OrderID = order.OrderID,
                    Carrier = order.Shipping.Carrier,
                    TrackingNumber = order.Shipping.TrackingNumber,
                    CustomerName = order.Shipping.CustomerName,
                    PhoneNumber = order.Shipping.PhoneNumber,
                    Latitude = order.Shipping.Latitude,
                    Longitude = order.Shipping.Longitude,
                    EstimatedDeliveryDate = order.Shipping.EstimatedDeliveryDate,
                    ShippingStatus = order.Shipping.ShippingStatus,
                    ShippingAddress = order.Shipping.ShippingAddress
                }
            }).ToList();

            return new PageResult<OrderDto>
            {
                Items = orderDtos,
                TotalCount = pagedOrders.TotalCount
            };
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrderAsync(string userId)
        {
            var orders = await _orderRepository.ListUserOrdersAsync(userId);
            return orders.Select(order => new OrderDto
            {
                Id = order.OrderID,
                UserID = order.UserID,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = order.OrderID,
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty
                }).ToList(),
                Shipping = new ShippingDTO
                {
                    Carrier = order.Shipping.Carrier,
                    TrackingNumber = order.Shipping.TrackingNumber,
                    CustomerName = order.Shipping.CustomerName,
                    PhoneNumber = order.Shipping.PhoneNumber,
                    Latitude = order.Shipping.Latitude,
                    Longitude = order.Shipping.Longitude,
                    EstimatedDeliveryDate = order.Shipping.EstimatedDeliveryDate,
                    ShippingStatus = order.Shipping.ShippingStatus,
                    ShippingAddress = order.Shipping.ShippingAddress
                }
            }).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = order.OrderID,
                UserID = order.UserID,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    OrderId = order.OrderID,
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty
                }).ToList(),
                Shipping = order.Shipping == null ? null : new ShippingDTO
                {
                    Carrier = order.Shipping.Carrier,
                    TrackingNumber = order.Shipping.TrackingNumber,
                    CustomerName = order.Shipping.CustomerName,
                    PhoneNumber = order.Shipping.PhoneNumber,
                    Latitude = order.Shipping.Latitude,
                    Longitude = order.Shipping.Longitude,
                    EstimatedDeliveryDate = order.Shipping.EstimatedDeliveryDate,
                    ShippingStatus = order.Shipping.ShippingStatus,
                    ShippingAddress = order.Shipping.ShippingAddress
                    
                }
            };
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, OrderDto orderDto)
        {
            var existingOrder = await _orderRepository.GetById(id);
            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.UserID = orderDto.UserID;
            existingOrder.OrderItems = orderDto.OrderItems.Select(oi => new OrderItem
            {
                Id = oi.Id,
                ProductID = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList();

            var updatedOrder = await _orderRepository.UpdateAsync(existingOrder);
            if (updatedOrder == null)
            {
                return null;
            }

            return new OrderDto
            {
                Id = updatedOrder.OrderID,
                UserID = updatedOrder.UserID,
                OrderItems = updatedOrder.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductID,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    ProductName = oi.Product?.Name ?? string.Empty
                }).ToList(),
                Shipping = updatedOrder.Shipping == null ? null : new ShippingDTO
                {
                    Carrier = updatedOrder.Shipping.Carrier,
                    TrackingNumber = updatedOrder.Shipping.TrackingNumber,
                    CustomerName = updatedOrder.Shipping.CustomerName,
                    PhoneNumber = updatedOrder.Shipping.PhoneNumber,
                    Latitude = updatedOrder.Shipping.Latitude,
                    Longitude = updatedOrder.Shipping.Longitude,
                    EstimatedDeliveryDate = updatedOrder.Shipping.EstimatedDeliveryDate,
                    ShippingAddress = updatedOrder.Shipping.ShippingAddress
                }
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
