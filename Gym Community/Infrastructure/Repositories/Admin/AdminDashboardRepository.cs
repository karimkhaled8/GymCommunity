using Gym_Community.API.DTOs.Admin;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gym_Community.Infrastructure.Repositories.Admin
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var totalSales = await _context.Orders.CountAsync();
            var totalProductsSold = await _context.OrderItems.SumAsync(i => i.Quantity);
            var activeGyms = await _context.Gym.CountAsync();
            var activeCoaches = await (
                from user in _context.Users
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                join role in _context.Roles on userRole.RoleId equals role.Id
                where role.Name == "Coach" && user.IsActive
                select user
            ).CountAsync();
            var premiumSubscribers = await _context.Users.CountAsync(u => u.IsPremium);
            var totalRevenue = await _context.Orders.SumAsync(o => o.Payment.Amount);

            var topProducts = await _context.OrderItems
                .GroupBy(i => i.Product.Name)
                .Select(g => new TopProductDto
                {
                    ProductName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();

            var salesTrend = await _context.Orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new DailySalesDto
                {
                    Date = g.Key,
                    TotalSales = g.Sum(x => x.Payment.Amount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return new DashboardSummaryDto
            {
                TotalSales = totalSales,
                TotalProductsSold = totalProductsSold,
                ActiveGyms = activeGyms,
                ActiveCoaches = activeCoaches,
                PremiumSubscribers = premiumSubscribers,
                TotalRevenue = totalRevenue,
                TopProducts = topProducts,
                SalesTrend = salesTrend
            };
        }
    }
}
