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
