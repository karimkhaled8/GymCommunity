using Gym_Community.API.DTOs.CoachStuff;

namespace Gym_Community.Application.Interfaces.CoachStuff
{
    public interface ICoachRatingService
    {
        Task<IEnumerable<CoachRatingDto>> GetByCoachIdAsync(string coachId);
        Task<bool> CreateAsync(CoachRatingDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
