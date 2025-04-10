using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Community.Domain.Models.Forum;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("AppUser")]
    public string UserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    [ForeignKey("Sub")]
    public int SubId { get; set; }
    public Sub Sub { get; set; } = null!;

    public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
