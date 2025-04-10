using Microsoft.Extensions.Hosting;

namespace Gym_Community.Domain.Data.Models.Forum;

public class Sub
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Post>? Posts { get; set; }
}
