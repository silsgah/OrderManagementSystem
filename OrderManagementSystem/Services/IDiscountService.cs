using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public interface IDiscountService
    {
        decimal GetDiscount(Customer customer, List<Order> orderHistory, Order newOrder);
    }
}
