using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound(new { success = false, message = "Payment not found" });
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDTO paymentDTO)
        {
            var payment = await _paymentService.CreatePaymentAsync(paymentDTO);
            if (payment != null)
            {
                return Ok(new { success = false, message = "Payment creation failed" });

            }
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PaymentStatus status)
        {
            var result = await _paymentService.UpdatePaymentStatusAsync(id, status);
            if (result)
            {
                return Ok(new { success = true, message = "Payment status updated successfully" });
            }
            return NotFound(new { success = false, message = "Payment not found" });
        }

     
    }
}
