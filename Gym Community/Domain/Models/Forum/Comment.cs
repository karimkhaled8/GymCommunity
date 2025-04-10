using Gym_Community.Domain.Models;

namespace Gym_Community.Domain.Models.Forum;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
