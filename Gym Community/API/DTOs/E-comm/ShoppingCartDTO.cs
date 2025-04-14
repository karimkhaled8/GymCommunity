namespace Gym_Community.API.DTOs.E_comm
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItemDTO> Items { get; set; } = new();
    }
}
