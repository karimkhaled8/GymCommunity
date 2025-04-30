using Gym_Community.API.DTOs.Coach.CoachStuff;
using Gym_Community.Application.Interfaces;
using Gym_Community.Domain.Models.CoachStuff;
using Gym_Community.Infrastructure.Interfaces.CoachStuff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Community.API.Controllers.Coach.CoachStuff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachOffersController : ControllerBase
    {
        private readonly ICoachOffersRepository _repository;
        private readonly IAwsService _awsService;

        public CoachOffersController(ICoachOffersRepository repository, IAwsService awsService)
        {
            _repository = repository;
            _awsService = awsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _repository.GetAllAsync();
            var dtos = offers.Select(o => new CoachOfferDto
            {
                Id = o.Id,
                Title = o.Title,
                Desc = o.Desc,
                Price = o.Price,
                ImageUrl = o.ImageUrl,
                DurationMonths = o.DurationMonths,
                CoachId = o.CoachId,
                CoachName = o.Coach?.LastName + " " + o.Coach?.FirstName
            });
            return Ok(dtos);
        }

        [HttpGet("coach/{coachId}")]
        public async Task<IActionResult> GetByCoachId(string coachId)
        {
            var offers = await _repository.GetByCoachIdAsync(coachId);
            var dtos = offers.Select(o => new CoachOfferDto
            {
                Id = o.Id,
                Title = o.Title,
                Desc = o.Desc,
                Price = o.Price,
                ImageUrl = o.ImageUrl,
                DurationMonths = o.DurationMonths,
                CoachId = o.CoachId,
                CoachName = o.Coach?.FirstName + " " + o.Coach?.LastName
            });
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Create([FromForm] CreateCoachOfferDto dto, [FromForm] IFormFile image)
        {
            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string imageUrl = string.Empty;
            if (image != null)
            {
                imageUrl = await _awsService.UploadFileAsync(image, "coachOffers");
            }

            var offer = new CoachOffers
            {
                Title = dto.Title,
                Desc = dto.Desc,
                Price = dto.Price,
                ImageUrl = imageUrl,
                DurationMonths = dto.DurationMonths,
                CoachId = coachId
            };

            var created = await _repository.AddAsync(offer);

            var resultDto = new CoachOfferDto
            {
                Id = created.Id,
                Title = created.Title,
                Desc = created.Desc,
                Price = created.Price,
                ImageUrl = created.ImageUrl,
                DurationMonths = created.DurationMonths,
                CoachId = created.CoachId,
                CoachName = created.Coach?.UserName
            };

            return Ok(resultDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Update( int id, [FromForm] CreateCoachOfferDto dto, [FromForm] IFormFile? image)
        {
            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (coachId == null) return Unauthorized();

            var existing = await _repository.GetByIdWithCoachAsync(id);
            if (existing == null) return NotFound();

            if (coachId != existing.CoachId)
            {
                return Unauthorized("This Offer is not yours to edit");
            }


            if (existing == null) return NotFound();

            if (image != null)
            {
                existing.ImageUrl = await _awsService.UploadFileAsync(image, "coachOffers");
            }

            existing.Title = dto.Title;
            existing.Desc = dto.Desc;
            existing.Price = dto.Price;
            existing.DurationMonths = dto.DurationMonths;
            


            var updated = await _repository.UpdateAsync(existing);

            var resultDto = new CoachOfferDto
            {
                Id = updated.Id,
                Title = updated.Title,
                Desc = updated.Desc,
                Price = updated.Price,
                ImageUrl = updated.ImageUrl,
                DurationMonths = updated.DurationMonths,
                CoachId = updated.CoachId,
                CoachName = updated.Coach?.UserName
            };

            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Delete(int id)
        {
            var coachId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existing = await _repository.GetByIdWithCoachAsync(id);
            if (existing == null) return NotFound();
            if (existing.CoachId != coachId) return Unauthorized("This Offer is not yours to delete.");

            await _repository.DeleteAsync(id);
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
