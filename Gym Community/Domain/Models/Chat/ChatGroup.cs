using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gym_Community.Domain.Models.Chat
{
    public class ChatGroup
    {
        [Key]
        public string GroupId { get; set; } // Unique group identifier
        public string GroupName { get; set; }
        public List<GroupMember> Members { get; set; } = new();
    }
}
