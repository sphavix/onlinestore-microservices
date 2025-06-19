using MongoDB.Driver;
using OrdersService.Application.Models;
using OrdersService.Application.Repositories;

namespace OrdersService.Infrastructure.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IMongoCollection<Order> _ordersCollection;
    private readonly string collectionName = "orders";
    public OrdersRepository(IMongoDatabase mongoDatabase)
    {
        _ordersCollection = mongoDatabase.GetCollection<Order>(collectionName);
    }
    public async Task<Order?> AddOrderAsync(Order order)
    {
        order.OrderID = Guid.NewGuid(); // Generate a new OrderID

        await _ordersCollection.InsertOneAsync(order);

        return order;
    }

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
       FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderId);

        var order = (await _ordersCollection.FindAsync(filter)).FirstOrDefault();

        if(order == null)
        {
            return false; // Order not found
        }

        // Delete the order
        DeleteResult deleteResult = await _ordersCollection.DeleteOneAsync(filter);

        return deleteResult.DeletedCount > 0;
    }

    public async Task<Order?> GetOrderConditionAsync(FilterDefinition<Order> filter)
    {
       return (await _ordersCollection.FindAsync(filter)).FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return (await _ordersCollection.FindAsync(Builders<Order>.Filter.Empty)).ToList(); // Get all orders
    }

    public async Task<IEnumerable<Order?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter)
    {
        return (await _ordersCollection.FindAsync(filter)).ToList();
    }

    public async Task<Order?> UpdateOrderAsync(Order order)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, order.OrderID);

        var existingOrder = (await _ordersCollection.FindAsync(filter)).FirstOrDefault();

        if (existingOrder == null)
        {
            return null; // Order not found
        }

        ReplaceOneResult result = await _ordersCollection.ReplaceOneAsync(filter, order);

        return result.IsAcknowledged ? order : null; // Return the updated order if the operation was acknowledged
    }
}
