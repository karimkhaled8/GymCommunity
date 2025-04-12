namespace Gym_Community.API.DTOs.Forum
{
    public class CommentCreateDTO
    {
        public string Content { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int PostId { get; set; }
    }
}
