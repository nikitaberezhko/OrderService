using BusModels;

namespace Services.Bus.Interfaces;

public interface ICreateOrderProducer
{
    Task NotifyOrderCreated(OrderCreatedMessage message);
}