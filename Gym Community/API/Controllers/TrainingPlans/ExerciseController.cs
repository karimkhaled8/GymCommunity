using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Infrastructure.Interfaces.Meals_and_Exercise;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Community.API.Controllers.TrainingPlans
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMuscleGroupRepository muscleGroupRepository;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseRepository exerciseRepository , IMapper mapper, IMuscleGroupRepository muscleGroupRepository)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
            this.muscleGroupRepository = muscleGroupRepository;
        }

        [HttpGet( "MuscleGroup")]
        public async Task<IActionResult> GetAllMuscleGroupsAasync()
        {
            IEnumerable<MuscleGroup> muscleGroups;
            muscleGroups = await muscleGroupRepository.GetAllAsync();

            var exerciseDtos = _mapper.Map<IEnumerable<MucleGroupDto>>(muscleGroups);
            return Ok(exerciseDtos);

        }

        [HttpGet]
        public async Task<IActionResult> GetExercises([FromQuery] int? muscleGroupId = null)
        {
            IEnumerable<Exercise> exercises;
            
            if (muscleGroupId.HasValue)
            {
                exercises = await _exerciseRepository.GetByMuscleGroupAsync(muscleGroupId.Value);
            }
            else
            {
                exercises = await _exerciseRepository.GetAllAsync();
            }
            
            var exerciseDtos = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return Ok(exerciseDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExercise(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            if (exercise == null) return NotFound();

            var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
            return Ok(exerciseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] ExerciseDto exerciseDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseDto);
            await _exerciseRepository.AddAsync(exercise);

            var createdExerciseDto = _mapper.Map<ExerciseDto>(exercise);
            return CreatedAtAction(nameof(GetExercise), new { id = exercise.Id }, createdExerciseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] ExerciseDto exerciseDto)
        {
            if (id != exerciseDto.Id) return BadRequest();

            var existingExercise = await _exerciseRepository.GetByIdAsync(id);
            if (existingExercise == null) return NotFound();

            _mapper.Map(exerciseDto, existingExercise);
            await _exerciseRepository.UpdateAsync(existingExercise);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(id);
            if (exercise == null) return NotFound();

            await _exerciseRepository.DeleteAsync(id);
            return NoContent();
        }
    }
} 