using MongoDB.Driver;
using OrdersService.Infrastructure.Models;

namespace OrdersService.Infrastructure.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order?> GetOrderConditionAsync(FilterDefinition<Order> filter);
    Task<IEnumerable<Order?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
    Task<Order?> AddOrderAsync(Order order);
    Task<Order?> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(Guid orderId);
}
