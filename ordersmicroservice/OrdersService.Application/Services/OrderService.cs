using AutoMapper;
using FluentValidation;
using MongoDB.Driver;
using OrdersService.Application.Dtos;
using OrdersService.Application.Validators;
using OrdersService.Infrastructure.Models;
using OrdersService.Infrastructure.Repositories;

namespace OrdersService.Application.Services;

public class OrderService(
    IOrdersRepository orderRepository, 
    IMapper mapper, 
    IValidator<CreateOrderRequest> createOrderValidator,
    IValidator<CreateOrderItemRequest> createOrderItemValidator,
    IValidator<UpdateOrderRequest> updateOrderValidator,
    IValidator<UpdateOrderItemRequest> updateOrderItemValidator) : IOrderService
{
    private readonly IOrdersRepository _orderRepository = orderRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateOrderRequest> _createOrderValidator = createOrderValidator;
    private readonly IValidator<CreateOrderItemRequest> _createOrderItemValidator = createOrderItemValidator;
    private readonly IValidator<UpdateOrderRequest> _updateOrderValidator = updateOrderValidator;
    private readonly IValidator<UpdateOrderItemRequest> _updateOrderItemValidator = updateOrderItemValidator;

    public async Task<OrderResponse?> CreateOrderAsync(CreateOrderRequest request)
    {
        // Check for the null object/request
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        // Validate the request with Fluent Validators
        var validateResults = await _createOrderValidator.ValidateAsync(request);

        if (!validateResults.IsValid)
        {
            string errors = string.Join(", ", validateResults.Errors.Select(x => x.ErrorMessage));

            throw new ArgumentException(errors);
        }

        // validate each order item because we cannot create order with no items
        foreach(CreateOrderItemRequest orderItemRequest in request.OrderItems)
        {
            var createOrderItemsvalidationResult = await _createOrderItemValidator.ValidateAsync(orderItemRequest);

            if(!createOrderItemsvalidationResult.IsValid)
            {
                string errors = string.Join(", ", validateResults.Errors.Select(x => x.ErrorMessage));

                throw new ArgumentException(errors);
            }
        }

        // TODO: Check the corresponding user by checking if the user ID exist by contacting the users/accounts microservice

        // Map data from Dto to entity
        var order = _mapper.Map<Order>(request);

        // Generate order items and compute totals
        foreach(OrderItem orderItem in order.OrderItems)
        {
            orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
        }
        order.TotalBill = order.OrderItems.Sum(total => total.TotalPrice);

        // Invoke the create method from the repository
        var newOrder = await _orderRepository.AddOrderAsync(order);

        if(newOrder == null)
        {
            return null;
        }

        // Finally map response entity back to Dto and return it
        var orderResponse = _mapper.Map<OrderResponse>(newOrder);

        return orderResponse;
    }

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.OrderID, orderId);

        var order = await _orderRepository.GetOrderConditionAsync(filter);

        if(order == null)
        {
            return false;
        }

        var isDeleted = await _orderRepository.DeleteOrderAsync(orderId);

        return isDeleted;
    }

    public async Task<OrderResponse?> GetOrderByCondiftionAsync(FilterDefinition<Order> filter)
    {
        var order = await _orderRepository.GetOrderConditionAsync(filter);

        if(order == null)
        {
            return null;
        }

        var orderResponse = _mapper.Map<OrderResponse>(order);

        return orderResponse;
    }

    public async Task<List<OrderResponse?>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetOrdersAsync();

        var orderResponse = _mapper.Map<List<OrderResponse?>>(orders);

        return orderResponse.ToList();
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondiftionAsync(FilterDefinition<Order> filter)
    {
        var orders = await _orderRepository.GetOrdersByConditionAsync(filter);

        var orderResponse = _mapper.Map<List<OrderResponse?>>(orders);   

        return orderResponse.ToList();
    }

    public async Task<OrderResponse?> UpdateOrderAsync(UpdateOrderRequest request)
    {
        // Check for the null object/request
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        // Validate the request with Fluent Validators
        var validateResults = await _updateOrderValidator.ValidateAsync(request);

        if (!validateResults.IsValid)
        {
            string errors = string.Join(", ", validateResults.Errors.Select(x => x.ErrorMessage));

            throw new ArgumentException(errors);
        }

        // validate each order item because we cannot create order with no items
        foreach (UpdateOrderItemRequest orderItemRequest in request.OrderItems)
        {
            var updateOrderItemsValidationResult = await _updateOrderItemValidator.ValidateAsync(orderItemRequest);

            if (!updateOrderItemsValidationResult.IsValid)
            {
                string errors = string.Join(", ", validateResults.Errors.Select(x => x.ErrorMessage));

                throw new ArgumentException(errors);
            }
        }

        // TODO: Check the corresponding user by checking if the user ID exist by contacting the users/accounts microservice

        // Map data from Dto to entity
        var order = _mapper.Map<Order>(request);

        // Generate order items and compute totals
        foreach (OrderItem orderItem in order.OrderItems)
        {
            orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
        }
        order.TotalBill = order.OrderItems.Sum(total => total.TotalPrice);

        // Invoke the create method from the repository
        var updatedOrder = await _orderRepository.UpdateOrderAsync(order);

        if (updatedOrder == null)
        {
            return null;
        }

        // Finally map response entity back to Dto and return it
        var orderResponse = _mapper.Map<OrderResponse>(updatedOrder);

        return orderResponse;
    }
}
