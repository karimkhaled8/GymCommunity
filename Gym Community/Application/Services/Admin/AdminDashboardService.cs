using Gym_Community.API.DTOs.Admin;
using Gym_Community.Application.Interfaces.Admin;
using Gym_Community.Infrastructure.Interfaces.Admin;

namespace Gym_Community.Application.Services.Admin
{
    public class AdminDashboardService: IAdminDashboardService
    {
        private readonly IAdminDashboardRepository _dashboardRepository;

        public AdminDashboardService(IAdminDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<UserMonthlyCountDto>> GetMonthlyUserCountByRoleAsync(string role, int year)
        {
            return await _dashboardRepository.GetMonthlyUserCountByRoleAsync(role, year);
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            return await _dashboardRepository.GetDashboardSummaryAsync();
        }
    }
}
