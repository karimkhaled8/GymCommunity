using Gym_Community.API.DTOs.Admin;

namespace Gym_Community.Application.Interfaces.Admin
{
    public interface IAdminDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync();
    }
}
