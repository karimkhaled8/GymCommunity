namespace Gym_Community.API.DTOs.Forum
{
    public class VoteReadDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = string.Empty;

        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public bool IsUpvote { get; set; }
    }
}
