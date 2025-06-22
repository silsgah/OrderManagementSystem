using OrderManagementSystem.Enum;

namespace OrderManagementSystem.Services
{
    public class DiscountStrategyFactory
    {
        private readonly Dictionary<CustomerSegment, IDiscountStrategy> _strategies;

        public DiscountStrategyFactory()
        {
            _strategies = new Dictionary<CustomerSegment, IDiscountStrategy>
            {
                { CustomerSegment.VIP, new VIPDiscountStrategy() },
                { CustomerSegment.Default, new RegularDiscountStrategy() },
                { CustomerSegment.Loyal, new LoyalDiscountStrategy() }
            };
        }

        public IDiscountStrategy GetStrategy(CustomerSegment segment)
        {
            return _strategies.TryGetValue(segment, out var strategy)
                ? strategy
                : new NoDiscountStrategy();
        }
    }
}
