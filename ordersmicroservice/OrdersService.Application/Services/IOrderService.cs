using MongoDB.Driver;
using OrdersService.Application.Dtos;
using OrdersService.Infrastructure.Models;

namespace OrdersService.Application.Services;

public interface IOrderService
{
    Task<List<OrderResponse?>> GetOrdersAsync();

    Task<List<OrderResponse?>> GetOrdersByCondiftionAsync(FilterDefinition<Order> filter);

    Task<OrderResponse?> GetOrderByCondiftionAsync(FilterDefinition<Order> filter);

    Task<OrderResponse?> CreateOrderAsync(CreateOrderRequest request);

    Task<OrderResponse?> UpdateOrderAsync(UpdateOrderRequest request);

    Task<bool> DeleteOrderAsync(Guid orderId);
}
