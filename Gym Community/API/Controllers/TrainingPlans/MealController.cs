using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;

        public MealController(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeals([FromQuery] bool? isSupplement = null)
        {
            IEnumerable<Meal> meals;
            
            if (isSupplement.HasValue)
            {
                meals = await _mealRepository.GetBySupplementStatusAsync(isSupplement.Value);
            }
            else
            {
                meals = await _mealRepository.GetAllAsync();
            }
            
            var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeal(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            if (meal == null) return NotFound();

            var mealDto = _mapper.Map<MealDto>(meal);
            return Ok(mealDto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMeals([FromQuery] string name, [FromQuery] bool? isSupplement = null)
        {
            var meals = await _mealRepository.GetByNameAsync(name, isSupplement);
            var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeal([FromBody] MealDto mealDto)
        {
            var meal = _mapper.Map<Meal>(mealDto);
            await _mealRepository.AddAsync(meal);

            var createdMealDto = _mapper.Map<MealDto>(meal);
            return CreatedAtAction(nameof(GetMeal), new { id = meal.Id }, createdMealDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMeal(int id, [FromBody] MealDto mealDto)
        {
            if (id != mealDto.Id) return BadRequest();

            var existingMeal = await _mealRepository.GetByIdAsync(id);
            if (existingMeal == null) return NotFound();

            _mapper.Map(mealDto, existingMeal);
            await _mealRepository.UpdateAsync(existingMeal);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _mealRepository.GetByIdAsync(id);
            if (meal == null) return NotFound();

            await _mealRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 