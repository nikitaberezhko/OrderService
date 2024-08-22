using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderValidatorTests;

public class GetOrdersInPeriodValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateValidatorForGetInPeriodCase();
        var model = new GetOrdersInPeriodModel
        { 
            End = DateTime.Now,
            Period = 30
        };
        
        // Act
        var actual = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(actual);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Period_Less_Than_28Days()
    {
        // Arrange
        var validator = CreateValidatorForGetInPeriodCase();
        var model = new GetOrdersInPeriodModel
        { 
            End = DateTime.Now,
            Period = 27
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    private OrderValidator CreateValidatorForGetInPeriodCase() =>
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            new Mock<IValidator<UpdateOrderModel>>().Object,
            new Mock<IValidator<DeleteOrderModel>>().Object,
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            Provider.Get<IValidator<GetOrdersInPeriodModel>>(),
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}