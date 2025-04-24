using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        public ShippingService(IShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }
        public async Task<Shipping?> CreateShippingAsync(Shipping shipping)
        {
            return await _shippingRepository.AddAsync(shipping);
        }

        public async Task<bool> DeleteShippingAsync(int id)
        {
            return await _shippingRepository.RemoveAsync(id);
        }

        public async Task<IEnumerable<Shipping>> GetAllShippingsAsync()
        {
            return await _shippingRepository.ListAsync();
        }

        public async Task<Shipping?> GetShippingByIdAsync(int id)
        {
            return await _shippingRepository.GetById(id);
        }

        public async Task<Shipping?> GetShippingByOrderIdAsync(int orderId)
        {
            return await _shippingRepository.GetByOrderId(orderId);
        }

        public async Task<Shipping?> UpdateShippingAsync(Shipping shipping)
        {
            return await _shippingRepository.UpdateAsync(shipping);
        }
    }
}
