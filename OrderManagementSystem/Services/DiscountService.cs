using OrderManagementSystem.Enum;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DiscountStrategyFactory _factory;

        public DiscountService()
        {
            _factory = new DiscountStrategyFactory();
        }

        public decimal GetDiscount(Customer customer, List<Order> orderHistory, Order newOrder)
        {
            if (customer == null || newOrder == null)
                return 0;

            var strategy = _factory.GetStrategy(customer.Segment);
            return strategy.CalculateDiscount(customer, orderHistory, newOrder);
        }
    }

}
