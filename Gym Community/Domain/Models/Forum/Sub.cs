using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Models.Forum;

public class Sub
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty; 
    public ICollection<Post>? Posts { get; set; } = new List<Post>();
}
