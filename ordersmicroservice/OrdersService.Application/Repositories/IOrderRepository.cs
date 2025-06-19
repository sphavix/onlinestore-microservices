using MongoDB.Driver;
using OrdersService.Application.Models;

namespace OrdersService.Application.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> GetOrderConditionAsync(FilterDefinition<Order> filter);
    Task<IEnumerable<Order?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
    Task<Order?> AddOrderAsync(Order order);
    Task<Order?> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(Guid orderId);
}
