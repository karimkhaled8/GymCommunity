namespace Gym_Community.API.DTOs.E_comm
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ProductID { get; set; }
        public string? UserID { get; set; }
        public string? ProductName { get; set; }
        public string? UserName { get; set; }
        public string? UserAvatar { get; set; } // Add this line
    }
}
