using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Enums;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Payment?> CreatePaymentAsync(PaymentDTO paymentDto)
        {
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                Currency = paymentDto.Currency,
                PaymentMethod = paymentDto.PaymentMethod,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            return await _paymentRepository.AddAsync(payment);
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetById(id);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int id, PaymentStatus status)
        {
            var payment  = await _paymentRepository.GetById(id);
            if (payment != null)
            {
                payment.Status = status;
                payment.UpdatedAt = DateTime.UtcNow;
                var updatedPayment = await _paymentRepository.UpdateAsync(payment);
                return updatedPayment != null;
            }
            return false;
        }
    }
}
