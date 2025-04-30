namespace Gym_Community.API.DTOs.Gym
{
    public class DashboardSummaryDTO
    {
        public int TotalGyms { get; set; }
        public int TotalMembers { get; set; }
        public List<MonthlyMembers> MonthlyMembers { get; set; } = new();
        public int ActivePlans { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<MonthlyRevenue> MonthlyRevenue { get; set; } = new();

        public decimal PlatformFees => Math.Round(TotalRevenue * 0.1M, 2);
        public decimal NetProfit => Math.Round(TotalRevenue * 0.9M, 2);
    }

    public class TopPlanDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Subscribers { get; set; }
    }

    public class RecentMemberDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
    }
    public class MonthlyRevenue
    {
        public string Month { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
    public class MonthlyMembers
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
