using AutoMapper;
using Gym_Community.API.DTOs.TrainingPlanDtos;
using Gym_Community.Domain.Data.Models.Meals_and_Exercise;
using Gym_Community.Domain.Models.Coach_Plans;

namespace Gym_Community.API.Mapping
{
    public class TrainingPlanProfile : Profile
    {
        public TrainingPlanProfile()
        {
            // DailyPlan mappings
            CreateMap<DailyPlan, DailyPlanDto>();
            CreateMap<CreateDailyPlanDto, DailyPlan>();
            CreateMap<UpdateDailyPlanDto, DailyPlan>();

            // WeekPlan mappings
            CreateMap<WeekPlan, WeekPlanDto>();
            CreateMap<CreateWeekPlanDto, WeekPlan>();
            CreateMap<UpdateWeekPlanDto, WeekPlan>();

            // TrainingPlan mappings
            CreateMap<TrainingPlan, TrainingPlanDto>();
            CreateMap<CreateTrainingPlanDto, TrainingPlan>();
            CreateMap<UpdateTrainingPlanDto, TrainingPlan>();

            // Exercise mappings
            CreateMap<Exercise, ExerciseDto>()
                .ForMember(dest => dest.MuscleGroupName, opt => opt.MapFrom(src => src.MuscleGroup.Name));

            // Meal mappings
            CreateMap<Meal, MealDto>();


            // Muscle Group mappings
            CreateMap<MuscleGroup, MucleGroupDto>();

        }
    }
} 