# 🧾 Order Management System – .NET 8 Web API

A clean and modular Web API implementation for managing customer orders, applying dynamic discounts, tracking order status, and generating analytics. Built with .NET 8, Entity Framework Core (In-Memory), and thoroughly tested with xUnit.

---

## ✅ Core Features

### 🏷️ Discounting System
- Implemented using the **Strategy Pattern**:
  - `VIPDiscountStrategy`: 15% off
  - `RegularDiscountStrategy`: 0% off
  - `PremiumDiscountStrategy`: 10–20% off based on spend
- Powered by a `DiscountStrategyFactory` that selects strategy dynamically based on `CustomerSegment`.

### 🔄 Order Status Tracking
- Order lifecycle managed with `OrderStatus` enum:
  - `Created`, `Processing`, `Shipped`, `Delivered`, `Cancelled`, `Pending`, `Completed`
- JSON serialization as string enabled:
  ```csharp
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public OrderStatus Status { get; set; }

---
## ✅ Features
- Create and retrieve customer orders
- Apply dynamic discount strategies based on customer segment and history
- Uses strategy pattern to support multiple discount logics (VIP, Loyal, etc.)
- Enum serialization as strings via `JsonStringEnumConverter`
- In-memory EF Core database
- Swagger documentation with XML comments
- Unit and integration tests

---

## 📦 API Endpoints

### `POST /api/orders`
Create a new order.
- Request Body:
```json
{
  "customerId": 1,
  "totalAmount": 950
}
```
- Response: `201 Created`

### `GET /api/orders/{id}`
Retrieve an order by ID.
- Response:
```json
{
  "id": 1,
  "customerId": 1,
  "status": "Created",
  "totalAmount": 950
}
```
### `GET /api/orders/{id}`
Update an order by ID.
- Response:
```json
{
  "id": 1,
  "customerId": 1,
  "status": "Completed",
  "totalAmount": 950,
}

---

## 🚀 Performance Optimization
This project implements in-memory caching to reduce redundant computations and improve response times:

✅ IMemoryCache is injected into the DiscountService to cache discount calculations per customer.

✅ The cache key is based on the customer ID to ensure scoped and relevant reuse.

✅ This avoids recalculating discounts for frequent or returning users within the cache window.

Benefits
⏱️ Reduces CPU usage and DB load for repeated discount computations

🔁 Enhances scalability for high-traffic usage

⚡ Improves user experience with faster responses

---
### technology used
| Technology            | Purpose                   |
| --------------------- | ------------------------- |
| .NET 8 Web API        | Backend Framework         |
| Entity Framework Core | ORM (In-Memory for tests) |
| xUnit                 | Testing Framework         |
| Swagger (Swashbuckle) | API Documentation         |
---
### testing
🧪 Testing
This solution includes both unit and integration tests using xUnit. All tests are located in the OrderManagementSystem.Tests project.

✔️ Unit Tests
File: DiscountServiceTests.cs

Tests include:

GetDiscount_ReturnsExpectedForVIPCustomer

GetDiscount_ReturnsExpectedForLoyalCustomer

GetDiscount_ReturnsExpectedForNewCustomer

Each test checks whether the right discount is applied based on customer segment and order history.

🔁 Integration Tests
File: OrderIntegrationTests.cs

Tests include:

PostOrder_CreatesOrderAndReturnsCreated: Verifies a full order lifecycle

Sends HTTP POST to /api/orders

Checks that:

The status code is 201 Created

The order is saved correctly

The discount is applied correctly

✅ The tests run against an in-memory database, so they are fast and isolated.



