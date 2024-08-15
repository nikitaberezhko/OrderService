using BusModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Bus.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class UpdateOrderProducer(
    IPublishEndpoint publishEndpoint,
    ILogger<UpdateOrderProducer> logger) 
    : IUpdateOrderProducer
{
    public async Task NotifyOrderUpdated(OrderUpdatedMessage message)
    {
        logger.LogInformation("Order updated with id: {id}", message.OrderId);
        await publishEndpoint.Publish(message);
    }
}