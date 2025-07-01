namespace OrdersService.Application.Dtos;

public record UpdateOrderItemRequest(Guid ProductID, decimal UnitPrice, int Quantity)
{
    public UpdateOrderItemRequest():this(default, default, default) { }
}
