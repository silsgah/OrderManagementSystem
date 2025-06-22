namespace OrderManagementSystem.DTOs
{
    public class OrderCreateRequest
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
