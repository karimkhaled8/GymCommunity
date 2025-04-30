using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IShippingRepository
    {
        Task<Shipping?> AddAsync(Shipping shipping);
        Task<IEnumerable<Shipping>> ListAsync();
        Task<Shipping?> GetById(int id);
        Task<Shipping> GetByOrderId(int orderId);
        Task<bool> UpdateAsync(int shippingId, string status);
        Task<bool> RemoveAsync(int id);
    }
}
