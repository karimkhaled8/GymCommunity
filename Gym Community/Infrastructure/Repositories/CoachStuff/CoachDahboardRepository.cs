using Gym_Community.API.DTOs.Admin;
using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.CoachStuff
{
    public enum DashboardTimeFilter
    {
        AllTime,
        ByYear,
        ByMonth
    }

    public class CoachDahboardRepository: ICoachDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        public CoachDahboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<CoachDahboardDTO> GetDashboardSummaryAsync(string coachId)
        {
            //total prof of products
         
            var totalProductsSold = await _context.OrderItems.Where(i=>i.Product.OwnerId==coachId).SumAsync(i => i.Quantity);

            //total revenu of products for this coach
            var totalRevenue = await _context.OrderItems.SumAsync(o => o.Price * o.Quantity);



            //total prof of plans

            var totalPlansSold = await _context.TrainingPlans.Where(tp=>tp.CoachId==coachId).SumAsync(tp=>tp.Payment.Amount);






            var topProducts = await _context.OrderItems.Where(i => i.Product.OwnerId == coachId)
                .GroupBy(i => i.Product.Name)
                .Select(g => new TopProductDto
                {
                    ProductName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();



            return new CoachDahboardDTO
            {
                TotalProductsSold = totalProductsSold,
                TotalPlansSoldRevenue = totalPlansSold,
                TotalRevenueProducts = totalRevenue,
                TopProducts = topProducts,
            };
        
        }


        public async Task<CoachDahboardDTO> GetDashboardSummaryAsync(string coachId, DashboardTimeFilter filter, int? year = null, int? month = null)
        {
            // Filter OrderItems and TrainingPlans based on time
            var orderItems = _context.OrderItems
                .Where(i => i.Product.OwnerId == coachId);

            var trainingPlans = _context.TrainingPlans
                .Where(tp => tp.CoachId == coachId);

            // Apply time filter
            if (filter == DashboardTimeFilter.ByYear && year.HasValue)
            {
                orderItems = orderItems.Where(i => i.Order.OrderDate.Year == year.Value);
                trainingPlans = trainingPlans.Where(tp => tp.Payment.CreatedAt.Year == year.Value);
            }
            else if (filter == DashboardTimeFilter.ByMonth && year.HasValue && month.HasValue)
            {
                orderItems = orderItems.Where(i => i.Order.OrderDate.Year == year.Value && i.Order.OrderDate.Month == month.Value);
                trainingPlans = trainingPlans.Where(tp => tp.Payment.CreatedAt.Year == year.Value && tp.Payment.CreatedAt.Month == month.Value);
            }

            // Total product quantity sold
            var totalProductsSold = await orderItems.SumAsync(i => (int?)i.Quantity) ?? 0;

            // Total revenue from products
            var totalRevenue = await orderItems.SumAsync(i => (decimal?)(i.Price * i.Quantity)) ?? 0m;

            // Total revenue from training plans
            var totalPlansSold = await trainingPlans.SumAsync(tp => (decimal?)tp.Payment.Amount) ?? 0m;

            // Top 5 products sold
            var topProducts = await orderItems
                .GroupBy(i => i.Product.Name)
                .Select(g => new TopProductDto
                {
                    ProductName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();

            return new CoachDahboardDTO
            {
                TotalProductsSold = totalProductsSold,
                TotalRevenueProducts = totalRevenue,
                TotalPlansSoldRevenue = totalPlansSold,
                TopProducts = topProducts,
            };
        }



    }
}
