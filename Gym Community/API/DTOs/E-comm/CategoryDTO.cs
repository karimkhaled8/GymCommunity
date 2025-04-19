using System.ComponentModel.DataAnnotations;

namespace Gym_Community.API.DTOs.E_comm
{
    public class CategoryDTO
    {
        public int? CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
