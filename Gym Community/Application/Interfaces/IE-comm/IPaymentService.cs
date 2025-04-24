using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Enums;

namespace Gym_Community.Application.Interfaces.IE_comm
{
    public interface IPaymentService
    {
        Task<Payment?> CreatePaymentAsync(PaymentDTO paymentDto);
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<bool> UpdatePaymentStatusAsync(int id, PaymentStatus status);
    }
}
