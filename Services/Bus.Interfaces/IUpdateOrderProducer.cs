using BusModels;

namespace Services.Bus.Interfaces;

public interface IUpdateOrderProducer
{
    Task NotifyOrderUpdated(OrderUpdatedMessage message);
}