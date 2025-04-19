using Gym_Community.Infrastructure.Context;
using Gym_Community.Infrastructure.Interfaces.Client;
using Microsoft.EntityFrameworkCore;

namespace Gym_Community.Infrastructure.Repositories.Client
{
    public class ClientInfoRepository : IClientInfoRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientInfoRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task AddClientInfoAsync(Domain.Models.ClientStuff.ClientInfo clientInfo)
        {
            
            await _context.ClientInfo.AddAsync(clientInfo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientInfoAsync(string userId)
        {
            var deleteClientInfo = await _context.ClientInfo.FirstOrDefaultAsync(x => x.Client == userId);
            if (deleteClientInfo != null)
            {
                _context.ClientInfo.Remove(deleteClientInfo);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Domain.Models.ClientStuff.ClientInfo?> GetClientInfoByUserIdAsync(string userId)
        {
            return _context.ClientInfo
                .Include(x => x.ClientUser)
                .FirstOrDefaultAsync(x => x.Client == userId);
        }

        public async Task UpdateClientInfoAsync( Domain.Models.ClientStuff.ClientInfo clientInfo)
        {
          
                _context.ClientInfo.Update(clientInfo);
                await _context.SaveChangesAsync();
            

        }
    }
}
