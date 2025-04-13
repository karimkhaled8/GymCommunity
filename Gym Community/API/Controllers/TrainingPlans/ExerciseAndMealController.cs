using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseAndMealController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;

        public ExerciseAndMealController(
            IExerciseRepository exerciseRepository,
            IMealRepository mealRepository,
            IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        // ===================== EXERCISES =====================

        [HttpGet("exercises")]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            var exerciseDtos = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return Ok(exerciseDtos);
        }

        [HttpGet("exercises/muscle-group/{muscleGroupId}")]
        public async Task<IActionResult> GetExercisesByMuscleGroup(int muscleGroupId)
        {
            var exercises = await _exerciseRepository.GetByMuscleGroupAsync(muscleGroupId);
            var exerciseDtos = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return Ok(exerciseDtos);
        }

        // ===================== MEALS =====================

        [HttpGet("meals")]
        public async Task<IActionResult> GetAllMeals()
        {
            var meals = await _mealRepository.GetAllAsync();
            var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpGet("meals/supplements/{isSupplement}")]
        public async Task<IActionResult> GetMealsBySupplementStatus(bool isSupplement)
        {
            var meals = await _mealRepository.GetBySupplementStatusAsync(isSupplement);
            var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
            return Ok(mealDtos);
        }

        [HttpGet("meals/search")]
        public async Task<IActionResult> SearchMeals([FromQuery] string name, [FromQuery] bool? isSupplement = null)
        {
            var meals = await _mealRepository.GetByNameAsync(name, isSupplement);
            var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
            return Ok(mealDtos);
        }
    }
} 