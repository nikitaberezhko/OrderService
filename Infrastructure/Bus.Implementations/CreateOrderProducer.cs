using BusModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Bus.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class CreateOrderProducer(
    IPublishEndpoint publishEndpoint,
    ILogger<CreateOrderProducer> logger) 
    : ICreateOrderProducer
{
    public async Task NotifyOrderCreated(OrderCreatedMessage message)
    {
        logger.LogInformation("Order created with id: {containers}", message.OrderId);
        await publishEndpoint.Publish(message);
    }
}