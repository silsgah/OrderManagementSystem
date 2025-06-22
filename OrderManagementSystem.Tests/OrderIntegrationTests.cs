
using System.Net.Http.Json;
using OrderManagementSystem.Models;
using OrderManagementSystem.Enum;
using OrderManagementSystem.Tests.TestHelpers;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Data;

namespace OrderManagementSystem.Tests
{
    public class OrderIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public OrderIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostOrder_CreatesOrderAndReturnsCreated()
        {
            var order = new Order
            {
                CustomerId = 1,
                TotalAmount = 1000,
                CreatedAt = DateTime.UtcNow,
                DeliveredAt = DateTime.UtcNow.AddMinutes(30),
                Status = OrderStatus.Completed
            };

            var response = await _client.PostAsJsonAsync("/api/orders", order);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdOrder = await response.Content.ReadFromJsonAsync<Order>();
            Assert.NotNull(createdOrder);
            Assert.True(createdOrder.TotalAmount < 1000); // Discount applied
        }

    }
}
