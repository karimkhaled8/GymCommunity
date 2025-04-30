using Gym_Community.API.DTOs.E_comm;

namespace Gym_Community.API.DTOs.Admin
{
    public class DashboardSummaryDto
    {
        public int TotalSales { get; set; }
        public int TotalProductsSold { get; set; }
        public int ActiveGyms { get; set; }
        public int ActiveCoaches { get; set; }
        public int PremiumSubscribers { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<TopProductDto> TopProducts { get; set; }
        public List<DailySalesDto> SalesTrend { get; set; }
    }
}
