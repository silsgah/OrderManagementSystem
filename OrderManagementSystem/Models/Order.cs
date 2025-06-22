using OrderManagementSystem.Enum;
using System.Text.Json.Serialization;

namespace OrderManagementSystem.Models
{
    /// <summary>
    /// Represents an order placed by a customer.
    /// </summary>
    public class Order
    {
        /// <summary>Order ID.</summary>
        public int Id { get; set; }

        /// <summary>When the order was created.</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>When the order was delivered, if applicable.</summary>
        public DateTime? DeliveredAt { get; set; }

        /// <summary>Total price before discount.</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Current order status.</summary>
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        /// <summary>ID of the customer who placed the order.</summary>
        public int CustomerId { get; set; }

        /// <summary>Navigation property to the customer.</summary>
        [JsonIgnore]
        public Customer? Customer { get; set; }
    }


}
