# 🧾 Order Management System – .NET 8 Web API

A clean and modular Web API implementation for managing customer orders, applying dynamic discounts, tracking order status, and generating analytics. Built with .NET 8, Entity Framework Core (In-Memory), and thoroughly tested with xUnit.

---

## ✅ Features

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

###  OrderManagementSystem/
├── Controllers/
│   └── OrdersController.cs
├── Models/
│   ├── Order.cs
│   └── Customer.cs
├── Services/
│   ├── DiscountService.cs
│   ├── DiscountStrategyFactory.cs
│   └── Strategies/*.cs
├── Tests/
│   ├── OrderIntegrationTests.cs
│   └── DiscountServiceTests.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Program.cs
└── README.md
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



