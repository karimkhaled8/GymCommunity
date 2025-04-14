using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService; 
       
        public OrderController(IOrderService orderService , IAuthService _authService)
        {
            _orderService = orderService;
        }

        [HttpGet("Admin/{ID}")]
        public async Task<IActionResult> Get(string UserId)
        {
            var role = await _authService.GetRole(UserId);
            if (role == null) return BadRequest(new { sucess = false, message = "Not Authantictated" });
            if (role == "Admin")
            {
                var orders = await _orderService.GetOrdersAsync();
                return Ok(orders);
            }
            else
            {
                return BadRequest(new { sucess = false, message = "Not Authantictated" });
            }
        }

        [HttpGet("user/{ID}")]
        public async Task<IActionResult> GetUserOrder(string UserId)
        {
            var role = await _authService.GetRole(UserId);
            if (role == null) return BadRequest(new { sucess = false, message = "Not Authantictated" });
            var orders = await _orderService.GetUserOrderAsync(UserId);    
            return Ok(orders);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0) return BadRequest(); 
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var order = await _orderService.CreateOrderAsync(orderDto);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id < 0) return BadRequest();
            var order = await _orderService.UpdateOrderAsync(id, orderDto);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) BadRequest();
            var order = await _orderService.RemoveOrderAsync(id);
            if (!order) NotFound();
            return Ok(new { sucess = true, message = "Deleted Successfully" });
        }
    }
}
