namespace Gym_Community.API.DTOs.Forum
{
    public class PostCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImgUrl { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int SubId { get; set; }

    }
}
