namespace Gym_Community.API.DTOs.E_comm
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public string? UserName { get; set; } // Optional, for display
        public int? ProductID { get; set; }
        public string? ProductName { get; set; } // Optional, for display
        public DateTime CreatedAt { get; set; }
    }
}
