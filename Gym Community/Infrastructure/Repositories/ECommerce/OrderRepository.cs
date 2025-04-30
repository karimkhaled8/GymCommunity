using Google.Apis.Util;
using Gym_Community.API.DTOs;
using Gym_Community.Domain.Data.Models.E_comm;
using Gym_Community.Domain.Enums;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.ECommerce
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
           _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            if (await _context.SaveChangesAsync() > 0)
            {
                return await GetById(order.OrderID);
            }
            else {
                return null; 
            }
        }

        public async Task<PageResult<Order>> ListAsync( string query, int page, int eleNo, string sort,ShippingStatus? status, DateOnly? date)
        {
            var ordersQuery = _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                string lowerQuery = query.ToLower();
                ordersQuery = ordersQuery.Where(o =>
                    o.OrderID.ToString().Contains(lowerQuery) ||
                    o.AppUser.Email.ToLower().Contains(lowerQuery));
            }

            if (status!=null)
            {
                ordersQuery = ordersQuery.Where(o => o.Shipping != null && o.Shipping.ShippingStatus == status);
            }

            if (date!=null)
            {
                var selectedDate = date.Value.ToDateTime(TimeOnly.MinValue); // Convert DateOnly to DateTime
                ordersQuery = ordersQuery.Where(o => o.OrderDate.Date == selectedDate.Date);
            }

            var totalCount = await ordersQuery.CountAsync();

            if (!string.IsNullOrEmpty(sort) && sort.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                ordersQuery = ordersQuery.OrderByDescending(o => o.OrderDate);
            }
            else
            {
                ordersQuery = ordersQuery.OrderBy(o => o.OrderDate);
            }

            var items = await ordersQuery
                .Skip((page - 1) * eleNo)
                .Take(eleNo)
                .ToListAsync();

            return new PageResult<Order>
            {
                Items = items,
                TotalCount = totalCount
            };
        }



        public async Task<IEnumerable<Order>> ListUserOrdersAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o=>o.AppUser)
                .Include(o => o.Payment)
                .Include(o=>o.Shipping)
                .Include(o=>o.OrderItems)
                    .ThenInclude(oi => oi.Product)  
                .ToListAsync(); 
        }

        public async Task<Order?> GetById(int id)
        {
            return await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) 
                .FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            if (await _context.SaveChangesAsync() > 0)
            {
                return await GetById(order.OrderID);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Order order)
        {

            if (order == null) return false;

            _context.Orders.Remove(order);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
