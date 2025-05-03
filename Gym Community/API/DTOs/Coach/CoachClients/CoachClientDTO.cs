using Gym_Community.Domain.Models;

namespace Gym_Community.API.DTOs.Coach.CoachClients
{
    public class CoachClientsDTO
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }


        public AppUser Client { get; set; }
    }
}
