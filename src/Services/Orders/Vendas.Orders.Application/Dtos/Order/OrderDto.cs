public record CreateOrderRequest(
    Guid CustomerId,
    List<OrderItemRequest> Items
);

public record OrderItemRequest(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
);

public record OrderResponse(
    Guid Id,
    Guid CustomerId,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt
);