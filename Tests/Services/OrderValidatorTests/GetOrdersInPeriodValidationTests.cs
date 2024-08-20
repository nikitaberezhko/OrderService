using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.OrderValidatorTests;

public class GetOrdersInPeriodValidationTests
{
    [Fact]
    public async Task GetOrdersInPeriod_MustBeValid()
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
    public async Task GetOrdersInPeriod_MustThrowBecausePeriodLessThan28Days()
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