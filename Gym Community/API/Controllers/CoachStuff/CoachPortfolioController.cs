using Gym_Community.API.DTOs.CoachStuff;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachPortfolioController : ControllerBase
    {
        private readonly ICoachPortfolioService _service;
        private readonly IAwsService _awsService;
        public CoachPortfolioController(ICoachPortfolioService service , IAwsService awsService)
        {
            _service = service;
            _awsService = awsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpGet("byCoach/{coachId}")]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var item = await _service.GetByCoachIdAsync(coachId);
            return item == null ? NotFound() : Ok(item);
        }

       
        [HttpPost]
        //added AboutMeImageUrl to send the image
        public async Task<IActionResult> Create([FromForm] CoachPortfolioDto dto,[FromForm] IFormFile AboutMeImageUrl)
        {

            string imageUrl = string.Empty;
            if (AboutMeImageUrl != null)
            {
                //folder location "ProfileImages" must be changed later
                imageUrl = await _awsService.UploadFileAsync(AboutMeImageUrl, "ProfileImages"); // save in aws and return url as strig 

            }
              var CoachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (CoachId == null) return Unauthorized();

            dto.CoachId = CoachId;
            dto.AboutMeImageUrl = imageUrl; // set the image url to the dto

            var success = await _service.CreateAsync(dto);
            return success ? Ok("CoachPortofolio added") : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CoachPortfolioDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
