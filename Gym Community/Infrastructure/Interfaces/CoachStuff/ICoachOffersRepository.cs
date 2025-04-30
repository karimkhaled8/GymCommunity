using Gym_Community.Domain.Models.CoachStuff;

namespace Gym_Community.Infrastructure.Interfaces.CoachStuff
{
    public interface ICoachOffersRepository
    {
        Task<IEnumerable<CoachOffers>> GetAllAsync();
        Task<IEnumerable<CoachOffers>> GetByCoachIdAsync(string coachId);
        //Task<CoachOffers> GetByIdAsync(int id);
        Task<CoachOffers> AddAsync(CoachOffers offer); 
        Task<CoachOffers> UpdateAsync(CoachOffers offer); 
        Task DeleteAsync(int id);
        Task<CoachOffers> GetByIdWithCoachAsync(int id); // new method


    }
}
