using BusModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Bus.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class DeleteOrderProducer(
    IPublishEndpoint publishEndpoint,
    ILogger<DeleteOrderProducer> logger) 
    : IDeleteOrderProducer
{
    public async Task NotifyOrderDeleted(OrderDeletedMessage message)
    {
        logger.LogInformation("Order deleted with container ids: {containers}", string.Join(", ", message.ContainerIds));
        await publishEndpoint.Publish(message);
    }
}