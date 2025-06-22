
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Enum;
using OrderManagementSystem.Models;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IDiscountService _discountService;

        public OrdersController(ApplicationDbContext db, IDiscountService discountService)
        {
            _db = db;
            _discountService = discountService;
        }

        /// <summary>
        /// Creates a new order for a customer.
        /// </summary>
        /// <param name="request">Order details.</param>
        /// <returns>The newly created order if successful, or a 404 if the customer does not exist.</returns>
        /// <response code="201">Order successfully created</response>
        /// <response code="404">Customer not found</response>
        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateOrder([FromBody] OrderCreateRequest request)
        {
            var customer = _db.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == request.CustomerId);
            if (customer == null)
                return NotFound("Customer not found");

            var orderHistory = customer.Orders.ToList();
            var discountAmount = _discountService.GetDiscount(customer, orderHistory, new Order { TotalAmount = request.TotalAmount });

            var newOrder = new Order
            {
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount - discountAmount,
                Status = OrderStatus.Pending
            };

            _db.Orders.Add(newOrder);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
        }


        /// <summary>
        /// Retrieves a specific order by ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order if found, or 404 otherwise.</returns>
        /// <response code="200">Order found</response>
        /// <response code="404">Order not found</response>
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Returns analytics and statistics about all orders.
        /// </summary>
        /// <returns>Aggregated analytics including revenue, order count, and fulfillment stats.</returns>
        /// <response code="200">Analytics generated</response>
        [HttpGet("analytics")]
        public IActionResult GetOrderAnalytics()
        {
            var orders = _db.Orders.Include(o => o.Customer).ToList();

            if (!orders.Any())
            {
                return Ok(new
                {
                    AverageOrderValue = 0,
                    AverageFulfillmentTime = "N/A",
                    TotalOrders = 0,
                    OrdersBySegment = new Dictionary<string, int>(),
                    CompletionRate = "0%",
                    LongestFulfillmentTime = "N/A",
                    ShortestFulfillmentTime = "N/A",
                    RevenueThisMonth = 0
                });
            }

            // Basic Stats
            var averageValue = orders.Average(o => o.TotalAmount);
            var totalOrders = orders.Count;

            var completedOrders = orders.Where(o => o.Status == OrderStatus.Completed).ToList();
            var completionRate = $"{(completedOrders.Count * 100) / totalOrders}%";

            var avgFulfillmentTicks = completedOrders.Any()
                ? completedOrders.Average(o => (DateTime.UtcNow - o.CreatedAt).Ticks)
                : 0;

            var avgFulfillmentTime = avgFulfillmentTicks > 0
                ? TimeSpan.FromTicks((long)avgFulfillmentTicks).ToString("g")
                : "N/A";

            var longest = completedOrders.Any()
                ? completedOrders.Max(o => (DateTime.UtcNow - o.CreatedAt))
                : (TimeSpan?)null;

            var shortest = completedOrders.Any()
                ? completedOrders.Min(o => (DateTime.UtcNow - o.CreatedAt))
                : (TimeSpan?)null;

            // Group by segment
            var ordersBySegment = orders
                .GroupBy(o => o.Customer.Segment.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            // Revenue for current month
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var revenueThisMonth = orders
                .Where(o => o.CreatedAt >= startOfMonth)
                .Sum(o => o.TotalAmount);

            // Result
            var result = new
            {
                AverageOrderValue = averageValue,
                AverageFulfillmentTime = avgFulfillmentTime,
                TotalOrders = totalOrders,
                OrdersBySegment = ordersBySegment,
                CompletionRate = completionRate,
                LongestFulfillmentTime = longest?.ToString("g") ?? "N/A",
                ShortestFulfillmentTime = shortest?.ToString("g") ?? "N/A",
                RevenueThisMonth = revenueThisMonth
            };

            return Ok(result);
        }

    }

}
