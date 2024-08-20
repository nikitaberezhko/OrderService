using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.OrderValidatorTests;

public class GetAllOrdersValidationTests
{
    [Fact]
    public async Task GetAllOrders_MustBeValid()
    {
        // Arrange
        var validator = CreateValidatorForGetAllCase();
        var model = new GetAllOrdersModel
        {
            Page = 1,
            PageSize = 10
        };

        // Act
        var actual = await validator.ValidateAsync(model);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public async Task GetAllOrders_MustThrowBecausePageLessThan1()
    {
        // Arrange
        var validator = CreateValidatorForGetAllCase();
        var model = new GetAllOrdersModel
        {
            Page = 0,
            PageSize = 10
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task GetAllOrders_MustThrowBecausePageSizeLessThan1()
    {
        // Arrange
        var validator = CreateValidatorForGetAllCase();
        var model = new GetAllOrdersModel
        {
            Page = 1,
            PageSize = 0
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task GetAllOrders_MustThrowBecausePageSizeGreaterThan50()
    {
        // Arrange
        var validator = CreateValidatorForGetAllCase();
        var model = new GetAllOrdersModel
        {
            Page = 1,
            PageSize = 51
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    private OrderValidator CreateValidatorForGetAllCase() =>
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            new Mock<IValidator<UpdateOrderModel>>().Object,
            new Mock<IValidator<DeleteOrderModel>>().Object,
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            Provider.Get<IValidator<GetAllOrdersModel>>());
}