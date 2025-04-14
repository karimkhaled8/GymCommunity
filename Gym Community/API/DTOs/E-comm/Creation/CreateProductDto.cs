using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Gym_Community.API.DTOs.E_comm
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
        public float? AverageRating { get; set; }
        public int? CategoryID { get; set; }
    }
}
