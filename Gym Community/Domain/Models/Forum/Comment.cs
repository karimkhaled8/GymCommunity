using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Forum;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AppUser")]
    public string UserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    [ForeignKey("Post")]
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
