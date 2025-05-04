using Gym_Community.API.DTOs.Admin;

namespace Gym_Community.API.DTOs.Coach.CoachStuff
{
    public class CoachDahboardDTO
    {
        public int TotalProductsSold { get; set; }
        public decimal TotalRevenueProducts { get; set; }
        public decimal TotalPlansSoldRevenue { get; set; }

        public IEnumerable<TopProductDto> TopProducts { get; set; } = new List<TopProductDto>();

    }
}
