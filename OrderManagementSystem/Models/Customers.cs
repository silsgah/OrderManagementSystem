using OrderManagementSystem.Enum;

namespace OrderManagementSystem.Models
{
    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer
    {
        /// <summary>Customer ID.</summary>
        public int Id { get; set; }

        /// <summary>Customer name.</summary>
        public string Name { get; set; }

        /// <summary>Customer segment type (e.g., VIP, Regular).</summary>
        public CustomerSegment Segment { get; set; }

        /// <summary>List of orders placed by the customer.</summary>
        public List<Order> Orders { get; set; } = new List<Order>();
    }

}
