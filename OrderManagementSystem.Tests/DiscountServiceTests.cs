using OrderManagementSystem.Enum;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Tests
{
    public class DiscountServiceTests
    {
        [Fact]
        public void GetDiscount_ReturnsCorrectDiscountForVIPCustomer()
        {
            // Arrange
            var service = new DiscountService();
            var customer = new Customer { Id = 1, Name = "John Doe", Segment = CustomerSegment.VIP };

            var history = new List<Order>
            {
                new Order { Id = 1, TotalAmount = 2000, Status = OrderStatus.Completed },
                new Order { Id = 2, TotalAmount = 3000, Status = OrderStatus.Completed }
            };

            var newOrder = new Order { TotalAmount = 1000 };

            // Act
            var discount = service.GetDiscount(customer, history, newOrder);

            // Assert
            Assert.True(discount > 0);
        }

        [Fact]
        public void GetDiscount_ReturnsZeroIfCustomerIsNull()
        {
            var service = new DiscountService();
            var result = service.GetDiscount(null, new List<Order>(), new Order { TotalAmount = 1000 });
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetDiscount_ReturnsZeroIfSegmentIsDefault()
        {
            var service = new DiscountService();
            var customer = new Customer { Id = 2, Name = "Jane Doe", Segment = CustomerSegment.Default };
            var history = new List<Order>();
            var order = new Order { TotalAmount = 1000 };

            var result = service.GetDiscount(customer, history, order);
            Assert.Equal(0, result);
        }


        [Fact]
        public void GetDiscount_ReturnsExpectedForLoyalCustomer()
        {
            // Arrange
            var service = new DiscountService();
            var customer = new Customer
            {
                Id = 2,
                Name = "Loyal Guy",
                Segment = CustomerSegment.Loyal
            };

            var pastOrders = new List<Order>
            {
             new Order { TotalAmount = 500, Status = OrderStatus.Completed },
             new Order { TotalAmount = 500, Status = OrderStatus.Completed },
             new Order { TotalAmount = 500, Status = OrderStatus.Completed }
             };

            var newOrder = new Order { TotalAmount = 1000 };

            // Act
            var discount = service.GetDiscount(customer, pastOrders, newOrder);

            // Assert
            Assert.Equal(50, discount); // if logic gives 5% for Regulars with 3+ orders
        }

    }
}
