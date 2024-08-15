using AutoMapper;
using BusModels;
using Domain;
using Exceptions.Contracts.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Bus.Interfaces;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;

namespace Services.Services.Implementations;

/// <summary>
/// Сервис заказов
/// </summary>
public class OrderService(
    IOrderRepository orderRepository,
    IMapper mapper,
    ICreateOrderProducer createOrderProducer,
    IDeleteOrderProducer deleteOrderProducer,
    IUpdateOrderProducer updateOrderProducer,
    IValidator<CreateOrderModel> createOrderValidator,
    IValidator<UpdateOrderModel> updateOrderValidator,
    IValidator<DeleteOrderModel> deleteOrderValidator,
    IValidator<GetOrderByIdModel> getOrderByIdValidator,
    IValidator<GetOrdersByClientIdModel> getOrdersByClientIdValidator,
    IValidator<GetOrdersInPeriodModel> getOrdersInPeriodValidator,
    IValidator<GetAllOrdersModel> getAllOrdersValidator) : IOrderService
{
    public async Task<Guid> Create(CreateOrderModel model)
    {
        var validationResult = await createOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };

        var id = await orderRepository.AddAsync(mapper.Map<Order>(model));
        var message = new OrderCreatedMessage
        {
            ContainerIds = model.Containers.Select(c => c.Id).ToList(),
            OrderId = id,
            EngagedUntil = model.DateEnd
        };
        await createOrderProducer.NotifyOrderCreated(message);
        
        return id;
    }
    
    public async Task<OrderModel> Update(UpdateOrderModel model)
    {
        var validationResult = await updateOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var order = await orderRepository.UpdateAsync(mapper.Map<Order>(model));
        var message = new OrderUpdatedMessage
        {
            ContainerIds = order.Containers.Select(c => c.Id).ToList(),
            OrderId = order.Id,
            EngagedUntil = order.DateEnd
        };
        await updateOrderProducer.NotifyOrderUpdated(message);
        
        var result = mapper.Map<OrderModel>(order);
        return result;
    }

    public async Task<OrderModel> Delete(DeleteOrderModel model)
    {
        var validationResult = await deleteOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var order = await orderRepository.DeleteAsync(mapper.Map<Order>(model));
        await deleteOrderProducer.NotifyOrderDeleted(new OrderDeletedMessage
        {
            ContainerIds = order.Containers.Select(c => c.Id).ToList(),
            OrderId = order.Id
        });
        
        var result = mapper.Map<OrderModel>(order);
        return result;
    }
    
    public async Task<OrderModel> GetById(GetOrderByIdModel model)
    {
        var validationResult = await getOrderByIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var order = await orderRepository.GetByIdAsync(mapper.Map<Order>(model));
        var result = mapper.Map<OrderModel>(order);
        return result;
    }
    
    public async Task<List<OrderModel>> GetByClientId(GetOrdersByClientIdModel model)
    {
        var validationResult = await getOrdersByClientIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var orders = await orderRepository.GetByClientIdAsync(mapper.Map<Order>(model));
        var result = mapper.Map<List<OrderModel>>(orders);
        return result;
    }
    
    public async Task<List<OrderFullModel>> GetByPeriod(GetOrdersInPeriodModel model)
    {
        var validationResult = await getOrdersInPeriodValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var orders = await orderRepository.GetByPeriodAsync(model.End.ToUniversalTime(), model.Period);
        var result = mapper.Map<List<OrderFullModel>>(orders);
        return result;
    }
    
    public async Task<List<OrderModel>> GetAll(GetAllOrdersModel model)
    {
        var validationResult = await getAllOrdersValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ServiceException
            {
                Title = "Model invalid",
                Message = "Model validation failed",
                StatusCode = StatusCodes.Status400BadRequest
            };
        
        var orders = await orderRepository.GetAllAsync(model.Page, model.PageSize);
        var result = mapper.Map<List<OrderModel>>(orders);
        return result;
    }
    
}