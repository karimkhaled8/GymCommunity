﻿using Gym_Community.API.DTOs;
using Gym_Community.API.DTOs.Admin;
using Gym_Community.Domain.Models;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gym_Community.Infrastructure.Repositories.Admin
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager; 

        public AdminDashboardRepository(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
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
                    QuantitySold = g.Sum(x => x.Quantity),
                    ProductImg = g.FirstOrDefault().Product.ImageUrl,
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

        public async Task<List<UserMonthlyCountDto>> GetMonthlyUserCountByRoleAsync(string role, int year)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            var result = users
                             .Where(u => u.CreatedAt.Year == year)
                             .GroupBy(u => u.CreatedAt.Month)
                             .Select(g => new UserMonthlyCountDto
                             {
                                 Month = g.Key,
                                 Count = g.Count()
                             })
                             .ToList();

            var fullYear = Enumerable.Range(1, 12)
                .Select(m => new UserMonthlyCountDto
                {
                    Month = m,
                    Count = result.FirstOrDefault(x => x.Month == m)?.Count ?? 0
                })
                .ToList();

            return fullYear;
        }
      

        public async Task<PageResult<AppUser>> GetUsers(
            string role, string query, bool? isActive, bool? isPremium, string gender,
            int pageNumber, int pageSize)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            var filteredUsers = users.AsQueryable();

            if (isActive.HasValue)
                filteredUsers = filteredUsers.Where(u => u.IsActive == isActive.Value);

            if (isPremium.HasValue)
                filteredUsers = filteredUsers.Where(u => u.IsPremium == isPremium.Value);

            if (!string.IsNullOrWhiteSpace(query))
            {
                var loweredQuery = query.ToLower();
                filteredUsers = filteredUsers.Where(u =>
                    (!string.IsNullOrEmpty(u.FirstName) && u.FirstName.ToLower().Contains(loweredQuery)) ||
                    (!string.IsNullOrEmpty(u.LastName) && u.LastName.ToLower().Contains(loweredQuery)) ||
                    (!string.IsNullOrEmpty(u.UserName) && u.UserName.ToLower().Contains(loweredQuery)) ||
                    (!string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(loweredQuery))
                );
            }

            if (!string.IsNullOrWhiteSpace(gender) && gender.ToLower() != "all")
                filteredUsers = filteredUsers.Where(u => u.Gender.ToLower() == gender.ToLower());

            var totalCount = filteredUsers.Count();
            var pagedUsers = filteredUsers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageResult<AppUser>
            {
                TotalCount = totalCount,
                Items = pagedUsers
            };
        }


    }
}
