using Domain;

namespace Services.Repositories.Interfaces;

/// <summary>
/// Репозиторий заказов
/// </summary>
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Order order);

    Task<List<Order>> GetByPeriodAsync(DateTime end, int days);

    Task<List<Order>> GetAllAsync(int page, int pageSize);

    Task<List<Order>> GetByClientIdAsync(Order order);
    
    Task<Guid> AddAsync(Order order);

    Task<Order> DeleteAsync(Order order);
    
    Task<Order> UpdateAsync(Order order);
}