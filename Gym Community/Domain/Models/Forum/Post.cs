using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Models.Forum;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public int SubId { get; set; }
    public Sub Sub { get; set; } = null!;

    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
