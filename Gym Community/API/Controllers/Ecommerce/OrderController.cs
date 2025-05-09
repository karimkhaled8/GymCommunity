﻿using Amazon.S3.Model.Internal.MarshallTransformations;
using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService; 
       
        public OrderController(IOrderService orderService , IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService; 
        }

        [Authorize]
        [HttpGet("admin")]
        public async Task<IActionResult> Get(string query = "", int page = 1, int eleNo = 10, string sort = "asc", ShippingStatus? status = null, DateOnly? date = null)
        {
            var userId = getUserID();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is null or token is invalid.");
            }

            var role = await _authService.GetRole(userId);
            if (string.IsNullOrEmpty(role))
            {
                return Unauthorized("Role not found or user does not exist.");
            }

            if (role != "Admin")
            {
                return Unauthorized("User is not an admin.");
            }

            var result = await _orderService.GetOrdersAsync(query, page, eleNo, sort, status, date);

            return Ok(new
            {
                success = true,
                data = result.Items,
                totalCount = result.TotalCount,
                totalPages = (int)Math.Ceiling(result.TotalCount / (double)eleNo),
                currentPage = page
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserOrder()
        {
            var userId = getUserID();
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            //var role = await _authService.GetRole(userId);
            //if (role == null) return BadRequest(new { sucess = false, message = "Not Authantictated" });
            var orders = await _orderService.GetUserOrderAsync(userId);    
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
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (getUserID() == null) return BadRequest("User ID is required"); 
            var order = await _orderService.CreateOrderAsync(orderDto, getUserID());
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
        private string getUserID()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }

    }
}
