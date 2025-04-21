using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces;
using Gym_Community.Domain.Models.Gym;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Gym
{
    public class GymRepository : IGymRepository
    {
        private readonly ApplicationDbContext _context;
        public GymRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Models.Gym.Gym?> AddAsync(Domain.Models.Gym.Gym gym)
        {
            _context.Gym.Add(gym);
            await _context.SaveChangesAsync();
            return gym;
        }

        public async Task<IEnumerable<Domain.Models.Gym.Gym>> ListAsync()
        {
            return await _context.Gym.Include(g => g.Owner).ToListAsync();
        }

        public async Task<Domain.Models.Gym.Gym?> GetByIdAsync(int id)
        {
            return await _context.Gym.Include(g => g.Owner).Where(g => g.Id==id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Domain.Models.Gym.Gym>> GetByOwnerIdAsync(string ownerId)
        {
            return await _context.Gym.Include(g => g.Owner).Where(g => g.OwnerId == ownerId).ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Gym.Gym>> GetNearbyGymsAsync(double lat, double lng, double radiusInKm)
        {
            var allGyms = await _context.Gym.ToListAsync();
            return allGyms.Where(g => GetDistance(lat, lng, g.Latitude, g.Longitude) <= radiusInKm);
        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Earth radius in KM
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            Console.WriteLine("Distance: " + R * c);
            return R * c;
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);

        public async Task<Domain.Models.Gym.Gym?> UpdateAsync(Domain.Models.Gym.Gym gym)
        {
            _context.Gym.Update(gym);
            await _context.SaveChangesAsync();
            return gym;
        }

        public async Task<bool> DeleteAsync(Domain.Models.Gym.Gym gym)
        {
            _context.Gym.Remove(gym);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
