namespace Gym_Community.API.DTOs.Forum
{
    public class PostReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int SubId { get; set; }
        public string SubName { get; set; } = string.Empty;
        public int CommentCount { get; set; }
        public int VoteCount { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
    }
}
