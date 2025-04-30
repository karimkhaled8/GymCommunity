using System.Numerics;
using Gym_Community.API.DTOs.Gym;
using Gym_Community.Application.Interfaces.Gym;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.Gym
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionService _service;
        public UserSubscriptionController(IUserSubscriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscriptionReadDTO>>> GetAll()
        {
            var allSub = await _service.GetAllAsync();
            return allSub == null? NotFound(): Ok(allSub);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscriptionReadDTO>> GetById(int id)
        {
            var sub = await _service.GetByIdAsync(id);
            return sub == null ? NotFound() : Ok(sub);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserSubscriptionReadDTO>>> GetByUserId(string userId)
        {
            var allSub = await _service.GetByUserIdAsync(userId);
            return allSub == null ? NotFound() : Ok(allSub);
        }

        [HttpGet("gym/{gymId}")]
        public async Task<ActionResult<IEnumerable<UserSubscriptionReadDTO>>> GetByGymId(int gymId)
        {
            var allSub = await _service.GetByGymIdAsync(gymId);
            return allSub == null ? NotFound() : Ok(allSub);
        }

        [HttpGet("plan/{planId}")]
        public async Task<ActionResult<IEnumerable<UserSubscriptionReadDTO>>> GetByPlanId(int planId)
        {
            var allSub = await _service.GetByPlanIdAsync(planId);
            return allSub == null ? NotFound() : Ok(allSub);
        }

        [HttpGet("ownerId/{ownerId}")]
        public async Task<ActionResult<UserSubscriptionReadDTO>> GetAllSubscriptionsByGymOwnerId(string ownerId)
        {
            var allSub = await _service.GetAllSubscriptionsByGymOwnerId(ownerId);
            return allSub == null ? NotFound() : Ok(allSub);
        }


        [HttpPost]
        public async Task<ActionResult<UserSubscriptionReadDTO>> Create(UserSubscriptionCreateDTO dto)
        {
            var newSub = await _service.CreateAsync(dto);
            return newSub == null ? NotFound() : Ok(newSub);
        }

        [HttpPut]
        public async Task<ActionResult<UserSubscriptionReadDTO>> Update(int id, UserSubscriptionUpdateDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            return await _service.DeleteAsync(id) ? Ok() : NotFound(); 
        }

        [HttpGet("validate/{qrCodeData}")]
        public async Task<ActionResult<UserSubscriptionReadDTO>> ValidateQrCode(string qrCodeData)
        {
            var result = await _service.ValidateQrCodeAsync(qrCodeData);
            return result == null ? Unauthorized("Invalid or expired QR code.") : Ok(result);
        }

    }
}
