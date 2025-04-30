using Gym_Community.API.DTOs.E_comm;
using Gym_Community.API.DTOs.E_comm.Creation;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.Payment_and_Shipping;
using Gym_Community.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        public ShippingController(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipping([FromBody] ShippingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var shipping = new Shipping
            {
                Carrier = dto.Carrier,
                TrackingNumber = dto.TrackingNumber,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
                ShippingStatus = dto.ShippingStatus,
                ShippingAddress = dto.ShippingAddress
            };
            var Createdshipping = await _shippingService.CreateShippingAsync(shipping);
            if (Createdshipping == null) return BadRequest();
            return Ok(Createdshipping); 
        }
       
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipping(int id)
        {
            var shipping = await _shippingService.GetShippingByIdAsync(id);
            if (shipping == null) return NotFound();
            return Ok(shipping);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetShippingByOrder(int orderId)
        {
            var shipping = await _shippingService.GetShippingByOrderIdAsync(orderId);
            if (shipping == null) return NotFound();
            return Ok(shipping);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var shippings = await _shippingService.GetAllShippingsAsync();
            return Ok(shippings);
        }

        // PUT api/<ShippingController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipping(int id, [FromBody] UpdateShippingStatusDto request)
        {
            if (id < 0) return BadRequest();
            var shipping = await _shippingService.GetShippingByIdAsync(id);
            if (shipping == null) return NotFound();

            var Updatedshipping = await _shippingService.UpdateShippingAsync(id,request.Status);
            if (Updatedshipping == null) return BadRequest();
            return Ok(Updatedshipping);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();
            var deleted = await _shippingService.DeleteShippingAsync(id);
            if (deleted == false) return BadRequest();
            return Ok(new { success = true, message = "Deleted Successfully" });
        }
    }
}
