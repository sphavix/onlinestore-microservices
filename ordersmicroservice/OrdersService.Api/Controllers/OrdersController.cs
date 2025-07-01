using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrdersService.Application.Dtos;
using OrdersService.Application.Services;
using OrdersService.Infrastructure.Models;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        //GET: /api/orders
        public async Task<ActionResult<IEnumerable<OrderResponse?>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            return Ok(orders);
        }

        // GET: /api/orders/search/order-id/{orderID}
        [HttpGet("search/order-id/{orderID:guid}")]
        public async Task<ActionResult<OrderResponse?>> GetOrderByOrderID(Guid orderID)
        {
            FilterDefinition<Order> filer = Builders<Order>.Filter.Eq(x => x.OrderID, orderID);

            var order = await _orderService.GetOrderByCondiftionAsync(filer);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: /api/orders/search/product-id/{productID}
        [HttpGet("search/product-id/{productID:guid}")]
        public async Task<ActionResult<OrderResponse?>> GetOrdersByProductID(Guid productID)
        {
            FilterDefinition<Order> filer = Builders<Order>.Filter.ElemMatch(x => x.OrderItems,
                Builders<OrderItem>.Filter.Eq(x => x.ProductID, productID)); // Checks through the elements of order items and find a matching product ID

            var order = await _orderService.GetOrdersByCondiftionAsync(filer);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: /api/orders/search/product-id/{productID}
        [HttpGet("search/orderDate/{orderDate}")]
        public async Task<ActionResult<OrderResponse?>> GetOrdersByOrderDate(DateTime orderDate)
        {
            FilterDefinition<Order> filer = Builders<Order>.Filter.Eq(x => x.OrderDate.ToString("yyy-MM-dd"),
                orderDate.ToString("yyy-MM-dd"));

            var order = await _orderService.GetOrdersByCondiftionAsync(filer);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: /api/orders/search/product-id/{productID}
        [HttpGet("search/userid/{userID}")]
        public async Task<ActionResult<OrderResponse?>> GetOrdersByUserID(Guid userID)
        {
            FilterDefinition<Order> filer = Builders<Order>.Filter.Eq(x => x.OrderDate.ToString("yyy-MM-dd"),
                orderDate.ToString("yyy-MM-dd"));

            var order = await _orderService.GetOrdersByCondiftionAsync(filer);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse?>> CreateOrder(CreateOrderRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid order request.");
            }

            var order = await _orderService.CreateOrderAsync(request);

            if (order is null)
            {
                return Problem("Failed to create order.");
            }
            return CreatedAtAction(nameof(GetOrderByOrderID), new { orderID = order.OrderID }, order);
        }

        [HttpPut("{orderID:guid}")]
        public async Task<ActionResult<OrderResponse?>> UpdateOrder(Guid orderID, UpdateOrderRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid order request.");
            }

            if (orderID != request.OrderID)
            {
                return BadRequest("Order ID mismatch.");
            }

            var order = await _orderService.UpdateOrderAsync(request);

            if (order is null)
            {
                return Problem("Failed to update order.");
            }

            return Ok(order);
        }

        [HttpDelete("{orderID:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                return BadRequest("Invalid order ID.");
            }

            var isDeleted = await _orderService.DeleteOrderAsync(orderID);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
