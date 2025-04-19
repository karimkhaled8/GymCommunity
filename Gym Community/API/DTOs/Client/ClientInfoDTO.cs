using Gym_Community.Domain.Enums;
using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.Client
{
    public class ClientInfoDTO
    {
        
        public int Id { get; set; } 
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int? WorkoutAvailability { get; set; } // 3 days a week, 5 days a week, etc.

       
        public string ClientId { get; set; }
        

        public ClientGoal? clientGoal { get; set; } // Enum for client goals
        public string? OtherGoal { get; set; } // if he/she has a different goal

        public int? bodyFat { get; set; }
        public string? Bio { get; set; }
        public string? CoverImg { get; set; }
    }
}
