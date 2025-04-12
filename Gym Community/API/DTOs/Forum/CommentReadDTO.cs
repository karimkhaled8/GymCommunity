namespace Gym_Community.API.DTOs.Forum
{
    public class CommentReadDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; } = null!;
        public int PostId { get; set; }
        public int VoteCount { get; set; }

    }
}
