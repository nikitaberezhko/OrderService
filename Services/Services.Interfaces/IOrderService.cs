using Services.Models.Request;
using Services.Models.Response;

namespace Services.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса заказов.
/// </summary>
public interface IOrderService
{
    Task<Guid> Create(CreateOrderModel model);

    Task<OrderModel> Update(UpdateOrderModel model);

    Task<OrderModel> Delete(DeleteOrderModel model);

    Task<OrderModel> GetById(GetOrderByIdModel model);

    Task<List<OrderModel>> GetByClientId(GetOrdersByClientIdModel model);

    Task<List<OrderFullModel>> GetByPeriod(GetOrdersInPeriodModel model);

    Task<List<OrderModel>> GetAll(GetAllOrdersModel model);
}