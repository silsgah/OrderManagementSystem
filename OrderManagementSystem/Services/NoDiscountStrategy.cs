using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            return 0;
        }
    }
}
