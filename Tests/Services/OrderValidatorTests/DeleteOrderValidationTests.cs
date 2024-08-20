using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.OrderValidatorTests;

public class DeleteOrderValidationTests
{
    [Fact]
    public async Task DeleteOrder_MustBeValid()
    {
        // Arrange
        var validator = CreateValidatorForDeleteCase();
        var model = new DeleteOrderModel
        {
            Id = Guid.NewGuid()
        };
        
        // Act
        var actual = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(actual);
    }

    [Fact]
    public async Task DeleteOrder_MustThrowBecauseIdIsInvalid()
    {
        // Arrange
        var validator = CreateValidatorForDeleteCase();
        var model = new DeleteOrderModel
        {
            Id = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    private OrderValidator CreateValidatorForDeleteCase() =>
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            new Mock<IValidator<UpdateOrderModel>>().Object,
            Provider.Get<IValidator<DeleteOrderModel>>(),
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}