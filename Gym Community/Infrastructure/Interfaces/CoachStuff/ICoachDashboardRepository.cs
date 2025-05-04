using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Infrastructure.Repositories.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface ICoachDashboardRepository
    {
        Task<CoachDahboardDTO> GetDashboardSummaryAsync(string coachId, DashboardTimeFilter filter, int? year = null, int? month = null);
        Task<CoachDahboardDTO> GetDashboardSummaryAsync(string coachId);

    }
}
