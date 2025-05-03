using Gym_Community.API.DTOs;
using Gym_Community.API.DTOs.Admin;
using Gym_Community.Domain.Models;

namespace Gym_Community.Infrastructure.Interfaces.Admin
{
    public interface IAdminDashboardRepository
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
        Task<List<UserMonthlyCountDto>> GetMonthlyUserCountByRoleAsync(string role, int year);
        Task<PageResult<AppUser>> GetUsers(string role, string query, bool? isActive, bool? isPremium, string gender, int pageNumber, int pageSize);


    }
}
