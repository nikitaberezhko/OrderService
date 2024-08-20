using AutoMapper;
using BusModels;
using Domain;
using Services.Bus.Interfaces;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;

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
    OrderValidator orderValidator) : IOrderService
{
    public async Task<Guid> Create(CreateOrderModel model)
    {
        await orderValidator.ValidateAsync(model);
        
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
        await orderValidator.ValidateAsync(model);
        
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
        await orderValidator.ValidateAsync(model);
        
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
        await orderValidator.ValidateAsync(model);
        
        var order = await orderRepository.GetByIdAsync(mapper.Map<Order>(model));
        var result = mapper.Map<OrderModel>(order);
        return result;
    }
    
    public async Task<List<OrderModel>> GetByClientId(GetOrdersByClientIdModel model)
    {
        await orderValidator.ValidateAsync(model);
        
        var orders = await orderRepository.GetByClientIdAsync(mapper.Map<Order>(model));
        var result = mapper.Map<List<OrderModel>>(orders);
        return result;
    }
    
    public async Task<List<OrderFullModel>> GetByPeriod(GetOrdersInPeriodModel model)
    {
        await orderValidator.ValidateAsync(model);
        
        var orders = await orderRepository.GetByPeriodAsync(model.End.ToUniversalTime(), model.Period);
        var result = mapper.Map<List<OrderFullModel>>(orders);
        return result;
    }
    
    public async Task<List<OrderModel>> GetAll(GetAllOrdersModel model)
    {
        await orderValidator.ValidateAsync(model);
        
        var orders = await orderRepository.GetAllAsync(model.Page, model.PageSize);
        var result = mapper.Map<List<OrderModel>>(orders);
        return result;
    }
    
}