using Gym_Community.Domain.Data.Models.Payment_and_Shipping;

namespace Gym_Community.Infrastructure.Interfaces.ECommerce
{
    public interface IPaymentRepository
    {
        Task<Payment?> AddAsync(Payment payment);
        Task<IEnumerable<Payment>> ListAsync();
        Task<Payment?> GetById(int id);
        Task<Payment?> UpdateAsync(Payment payment);
        Task<bool> RemoveAsync(int id);
    }
}
