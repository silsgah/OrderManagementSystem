using OrderManagementSystem.Enum;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class VIPDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            return newOrder.TotalAmount * 0.15m;
        }
    }

    public class RegularDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            return 0; 
        }
    }

    public class PremiumDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            var totalSpend = orderHistory.Sum(o => o.TotalAmount);
            return totalSpend > 10000 ? newOrder.TotalAmount * 0.20m : newOrder.TotalAmount * 0.10m;
        }
    }
    public class LoyalDiscountStrategy : IDiscountStrategy
    {
        public decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            var completedOrders = orderHistory.Count(o => o.Status == OrderStatus.Completed);
            return completedOrders >= 3 ? newOrder.TotalAmount * 0.05m : 0;
        }
    }

}
