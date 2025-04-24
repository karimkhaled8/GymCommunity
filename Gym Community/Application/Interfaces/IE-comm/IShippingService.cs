using Gym_Community.Domain.Data.Models.Payment_and_Shipping;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IShippingService
    {
        Task<Shipping?> CreateShippingAsync(Shipping shipping);
        Task<Shipping?> GetShippingByIdAsync(int id);
        Task<Shipping?> GetShippingByOrderIdAsync(int orderId);
        Task<IEnumerable<Shipping>> GetAllShippingsAsync();
        Task<Shipping?> UpdateShippingAsync(Shipping shipping);
        Task<bool> DeleteShippingAsync(int id);
    }
}
