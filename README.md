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
### Project structure
# Order Management System

A minimal .NET 8 Web API project for managing customer orders with integrated discount logic. This project demonstrates clean architecture practices, strategy pattern for discount calculation, enum serialization, Swagger integration, and automated testing.

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

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Running the App
```bash
# Restore packages
dotnet restore

# Run the API
dotnet run --project OrderManagementSystem
```

Visit `https://localhost:<port>/swagger` to test endpoints.

---

## 📦 API Endpoints

### `POST /api/orders`
Create a new order.
- Request Body:
```json
{
  "customerId": 1,
  "totalAmount": 1000,
  "createdAt": "2024-01-01T00:00:00Z",
  "deliveredAt": "2024-01-01T00:30:00Z",
  "status": "Pending"
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
  "totalAmount": 950,
  ...
}
```

### `GET /api/orders/analytics`
Returns order count and average amount per status.

---

## ✅ Testing

Run tests using:
```bash
dotnet test
```

The project contains:
- **Unit tests** (`DiscountServiceTests.cs`) to verify discount strategies
- **Integration tests** (`OrderIntegrationTests.cs`) for full API flow

---

## 📁 Project Structure

```bash
OrderManagementSystem/
├── Controllers/
│   └── OrdersController.cs            # Handles API endpoints for order operations
├── Data/
│   └── ApplicationDbContext.cs        # In-memory EF Core DB context
├── Enum/
│   ├── CustomerSegment.cs             # Enum for customer segmentation
│   └── OrderStatus.cs                 # Enum for order lifecycle status
├── Models/
│   ├── Customer.cs                    # Represents a customer
│   └── Order.cs                       # Represents an order and relationship to customer
├── Services/
│   ├── IDiscountService.cs            # Interface for discount logic
│   ├── DiscountService.cs             # Logic to choose appropriate discount strategy
│   └── Strategies/                    # Contains different discount strategy implementations
│       ├── VIPDiscountStrategy.cs
│       ├── RegularDiscountStrategy.cs
│       ├── PremiumDiscountStrategy.cs
│       └── DiscountStrategyFactory.cs
├── Program.cs                         # Entry point with Swagger, JSON settings, seeding
├── OrderManagementSystem.csproj       # Project definition
└── README.md                          # 📘 Project documentation

OrderManagementSystem.Tests/
├── DiscountServiceTests.cs            # Unit tests for DiscountService logic
└── OrderIntegrationTests.cs           # Integration tests for order posting
```

---

## 👨‍💻 Author
Silas AWS

Feel free to contribute or suggest improvements!

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



