using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.OrderValidatorTests;

public class GetOrderByIdValidationTests
{
    [Fact]
    public async Task GetOrderById_MustBeValid()
    {
        // Arrange
        var validator = CreateValidatorForGetByIdCase();
        var model = new GetOrderByIdModel
        {
            Id = Guid.NewGuid()
        };
        
        // Act
        var actual = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(actual);
    }

    [Fact]
    public async Task GetOrderById_MustThrowBecauseIdIsInvalid()
    {
        // Arrange
        var validator = CreateValidatorForGetByIdCase();
        var model = new GetOrderByIdModel
        {
            Id = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    private OrderValidator CreateValidatorForGetByIdCase() => 
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            new Mock<IValidator<UpdateOrderModel>>().Object,
            new Mock<IValidator<DeleteOrderModel>>().Object,
            Provider.Get<IValidator<GetOrderByIdModel>>(),
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}