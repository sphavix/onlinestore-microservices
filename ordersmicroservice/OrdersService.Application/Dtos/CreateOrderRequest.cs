namespace OrdersService.Application.Dtos;

public record CreateOrderRequest(Guid UserID, DateTime OrderDate, List<CreateOrderItemRequest> OrderItems)
{
    public CreateOrderRequest(): this(default, default, default) { }
}
