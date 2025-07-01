namespace OrdersService.Application.Dtos;

public record UpdateOrderRequest(Guid OrderID, Guid UserID, DateTime OrderDate, List<UpdateOrderItemRequest> OrderItems)
{
    public UpdateOrderRequest():this(default, default, default, default) {}
}
