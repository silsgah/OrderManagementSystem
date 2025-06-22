using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(Customer customer, List<Order> orderHistory, Order newOrder);
    }

}
