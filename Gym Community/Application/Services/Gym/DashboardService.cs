using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Gym;

namespace Gym_Community.Application.Services.Gym
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDashboardRepository _repo;
        public DashboardService(ApplicationDbContext context, IDashboardRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        public async Task<List<RecentMemberDTO>> GetRecentMembers(string gymOwner)
        {
            return await _repo.GetRecentMembers(gymOwner);
        }

        public async Task<DashboardSummaryDTO> GetSummary(string gymOwner)
        {
            return await _repo.GetSummary(gymOwner);
        }

        public async Task<List<TopPlanDTO>> GetTopPlans(string gymOwner)
        {
            return await _repo.GetTopPlans(gymOwner);
        }
    }
}
