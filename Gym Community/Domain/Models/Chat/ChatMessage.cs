namespace Gym_Community.Domain.Models.Chat
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; } // Changed from ReceiverId to GroupId
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
