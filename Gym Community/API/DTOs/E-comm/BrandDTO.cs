using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.E_comm
{
    public class BrandDTO
    {
        public int BrandID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
