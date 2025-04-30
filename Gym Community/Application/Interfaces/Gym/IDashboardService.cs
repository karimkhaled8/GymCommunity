using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Application.Interfaces.Gym
{
    public interface IDashboardService
    {
        public Task<DashboardSummaryDTO> GetSummary(string gymOwner);
        public Task<List<TopPlanDTO>> GetTopPlans(string gymOwner);
        public Task<List<RecentMemberDTO>> GetRecentMembers(string gymOwner);
    }
}
