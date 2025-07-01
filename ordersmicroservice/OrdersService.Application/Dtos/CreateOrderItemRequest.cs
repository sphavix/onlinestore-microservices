namespace OrdersService.Application.Dtos;

public record CreateOrderItemRequest(Guid ProductID, decimal UnitPrice, int Quantity)
{
    public CreateOrderItemRequest() : this(default, default, default) { }
}
