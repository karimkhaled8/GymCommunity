using Gym_Community.API.DTOs.Admin;

namespace Gym_Community.Infrastructure.Interfaces.Admin
{
    public interface IAdminDashboardRepository
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();

    }
}
