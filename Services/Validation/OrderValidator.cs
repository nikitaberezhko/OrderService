using Exceptions.Contracts.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Models.Request;

namespace Services.Validation;

public class OrderValidator(
    IValidator<CreateOrderModel> createOrderValidator,
    IValidator<UpdateOrderModel> updateOrderValidator,
    IValidator<DeleteOrderModel> deleteOrderValidator,
    IValidator<GetOrderByIdModel> getOrderByIdValidator,
    IValidator<GetOrdersByClientIdModel> getOrdersByClientIdValidator,
    IValidator<GetOrdersInPeriodModel> getOrdersInPeriodValidator,
    IValidator<GetAllOrdersModel> getAllOrdersValidator)
{
    public async Task<bool> ValidateAsync(CreateOrderModel model)
    {
        var validationResult = await createOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(UpdateOrderModel model)
    {
        var validationResult = await updateOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(DeleteOrderModel model)
    {
        var validationResult = await deleteOrderValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetOrderByIdModel model)
    {
        var validationResult = await getOrderByIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetOrdersByClientIdModel model)
    {
        var validationResult = await getOrdersByClientIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetOrdersInPeriodModel model)
    {
        var validationResult = await getOrdersInPeriodValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetAllOrdersModel model)
    {
        var validationResult = await getAllOrdersValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }

    private void ThrowWithStandartMessage()
    {
        throw new ServiceException
        {
            Title = "Model invalid",
            Message = "Model validation failed",
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}