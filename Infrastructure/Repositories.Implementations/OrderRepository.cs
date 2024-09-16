using Domain;
using Exceptions.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class OrderRepository(DbContext context) : IOrderRepository
{
    public async Task<Order> GetByIdAsync(Order order)
    {
        var result = await context.Set<Order>()
            .FirstOrDefaultAsync(x => x.Id == order.Id && x.IsDeleted == false);
        if (result != null)
            return result;
        
        throw new InfrastructureException
        {
            Title = "Order not found",
            Message = $"Order with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
    
    public async Task<List<Order>> GetAllAsync(int page, int pageSize)
    {
        return await context.Set<Order>()
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByPeriodAsync(DateTime end, int days)
    {
        var result = await context.Set<Order>()
            .Where(x => x.DateEnd < end  
                        && x.DateEnd > end.Subtract(new TimeSpan(days, 0, 0, 0))
                        && !x.IsDeleted).ToListAsync();
        
        return result;
    }

    public async Task<List<Order>> GetByClientIdAsync(Order order)
    {
        var result = await context.Set<Order>()
            .Where(x => x.ClientId == order.ClientId && x.IsDeleted == false).ToListAsync();
        
        return result;
    }
    
    public async Task<Guid> AddAsync(Order order)
    {
        var container = await context.Set<Container>()
            .Where(x => order.Containers
                .Select(x => x.Id)
                .Contains(x.Id))
            .FirstOrDefaultAsync();
        
        if (container == null)
        {
            order.Id = Guid.NewGuid();
            await context.Set<Order>().AddAsync(order);
            await context.SaveChangesAsync();

            return order.Id;
        }

        throw new InfrastructureException
        {
            Title = "Containers already used",
            Message = $"Container with this ids is engaged",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<Order> DeleteAsync(Order order)
    {
        var result = await context.Set<Order>()
            .Include(x => x.DownTimes)
            .Include(x => x.Containers)
            .FirstOrDefaultAsync(x => x.Id == order.Id && x.IsDeleted == false);

        if (result != null)
        {
            await context.Set<Order>().ExecuteUpdateAsync(
                s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<DownTime>().Where(x => x.OrderId == order.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDeleted, true));
            await context.Set<Container>().Where(x => x.OrderId == order.Id).ExecuteDeleteAsync();
            await context.SaveChangesAsync(); 
            
            return result;
        }

        throw new InfrastructureException
        {
            Title = "Order not found",
            Message = $"Order with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var result = await context.Set<Order>()
            .Include(x => x.DownTimes)
            .Include(x => x.Containers)
            .FirstOrDefaultAsync(x => x.Id == order.Id && x.IsDeleted == false);
        
        if (result != null)
        {
            order.ClientId = result.Id;
            context.Entry(result).CurrentValues.SetValues(order);
            ChangeOrderIdInCollection(order);

            context.Set<Container>().RemoveRange(result.Containers);
            await context.SaveChangesAsync();
            
            await CheckContainersExistAndThrow(order.Containers);
            await context.Set<Container>().AddRangeAsync(order.Containers);
            context.Set<DownTime>().RemoveRange(result.DownTimes);
            await context.Set<DownTime>().AddRangeAsync(order.DownTimes);
            
            await context.SaveChangesAsync();
            return result;
        }
        
        throw new InfrastructureException
        {
            Title = "Order not found",
            Message = $"Order with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    private void ChangeOrderIdInCollection(Order order)
    {
        foreach (var c in order.Containers) 
            c.OrderId = order.Id;
        foreach (var d in order.DownTimes) 
            d.OrderId = order.Id;
    }

    private async Task CheckContainersExistAndThrow(ICollection<Container> containers)
    {
        var container = await context.Set<Container>()
            .Where(x => containers
                .Select(x => x.Id)
                .Contains(x.Id))
            .FirstOrDefaultAsync();

        if (container != null)
            throw new InfrastructureException
            {
                Title = "Containers already used",
                Message = $"Container with this ids is engaged",
                StatusCode = StatusCodes.Status404NotFound
            };
    }
}