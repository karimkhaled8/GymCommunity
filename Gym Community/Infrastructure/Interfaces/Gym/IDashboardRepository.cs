using Gym_Community.API.DTOs.Gym;

namespace Gym_Community.Infrastructure.Interfaces.Gym
{
    public interface IDashboardRepository
    {
        public Task<DashboardSummaryDTO> GetSummary(string gymOwner);
        public Task<List<TopPlanDTO>> GetTopPlans(string gymOwner);
        public Task<List<RecentMemberDTO>> GetRecentMembers(string gymOwner);
    }
}
