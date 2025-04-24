using Gym_Community.Domain.Data.Models.Payment_and_Shipping;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IShippingRepository
    {
        Task<Shipping?> AddAsync(Shipping shipping);
        Task<IEnumerable<Shipping>> ListAsync();
        Task<Shipping?> GetById(int id);
        Task<Shipping> GetByOrderId(int orderId);
        Task<Shipping?> UpdateAsync(Shipping shipping);
        Task<bool> RemoveAsync(int id);
    }
}
