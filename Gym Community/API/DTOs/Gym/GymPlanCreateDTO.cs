namespace Gym_Community.API.DTOs.Gym
{
    public class GymPlanCreateDTO
    {
        public int GymId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public bool HasPrivateCoach { get; set; }
        public bool HasNutritionPlan { get; set; }
        public bool HasAccessToAllAreas { get; set; }
    }

    public class GymPlanReadDTO
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public bool HasPrivateCoach { get; set; }
        public bool HasNutritionPlan { get; set; }
        public bool HasAccessToAllAreas { get; set; }
        public int NoOfSubscriptions { get; set; }
    }

}
