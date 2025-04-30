using Gym_Community.API.DTOs.Gym;
using System;
using Gym_Community.Infrastructure.Interfaces.Gym;
using Gym_Community.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class DashboardRepository:IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardSummaryDTO> GetSummary(string gymOwnerId)
        {
            var totalGyms = await _context.Gym.CountAsync(g => g.OwnerId == gymOwnerId);
            var totalMembers = await _context.UserSubscriptions.CountAsync(m => m.Gym.OwnerId == gymOwnerId);
            var activePlans = await _context.GymPlans.CountAsync(p => p.Gym.OwnerId == gymOwnerId);
            var totalRevenue = await _context.UserSubscriptions
                .Where(p => p.Gym.OwnerId == gymOwnerId && p.paymentStatus == PaymentStatus.Completed)
                .SumAsync(p => p.Plan.Price);

            var monthlyRevenue = await _context.UserSubscriptions
                .Where(p => p.Gym.OwnerId == gymOwnerId && p.paymentStatus == PaymentStatus.Completed)
                .GroupBy(p => new { p.PurchaseDate.Year, p.PurchaseDate.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Take(6)
                .Select(g => new MonthlyRevenue
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    Amount = g.Sum(p => p.Plan.Price)
                })
                .ToListAsync();

            var monthlyMembers = await _context.UserSubscriptions
                .Where(m => m.Gym.OwnerId == gymOwnerId)
                .GroupBy(m => new { m.PurchaseDate.Year, m.PurchaseDate.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Take(6)
                .Select(g => new MonthlyMembers
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    Count = g.Count()
                })
                .ToListAsync();

            return new DashboardSummaryDTO
            {
                TotalGyms = totalGyms,
                TotalMembers = totalMembers,
                ActivePlans = activePlans,
                TotalRevenue = totalRevenue,
                MonthlyRevenue = monthlyRevenue,
                MonthlyMembers = monthlyMembers
            };
        }


        public async Task<List<TopPlanDTO>> GetTopPlans(string ownerId)
        {
            return await _context.GymPlans
                .OrderByDescending(p => p.UserSubscriptions.Where(s=>s.Gym.OwnerId == ownerId).Count())
                .Take(5)
                .Select(p => new TopPlanDTO
                {
                    Name = p.Title,
                    Subscribers = p.UserSubscriptions.Where(s => s.Gym.OwnerId == ownerId).Count()
                }).ToListAsync();
        }

        public async Task<List<RecentMemberDTO>> GetRecentMembers(string ownerId)
        {
            return await _context.UserSubscriptions
                .Where(s => s.Gym.OwnerId == ownerId)
                .OrderByDescending(s => s.PurchaseDate)
                .Take(5)
                .Select(s => new RecentMemberDTO
                {
                    Name = s.User.FirstName + " " + s.User.LastName,
                    Plan = s.Plan.Title,
                    JoinDate = s.PurchaseDate
                }).ToListAsync();
        }
    }
}
