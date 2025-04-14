namespace Gym_Community.API.DTOs.E_comm
{
    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
