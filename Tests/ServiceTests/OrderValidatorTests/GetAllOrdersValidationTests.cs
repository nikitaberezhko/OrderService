using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderValidatorTests;

public class GetAllOrdersValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
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
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Page_Less_Than_1()
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
    public async Task ValidateAsync_Should_Throw_ServiceException_PageSize_Less_Than_1()
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
    public async Task ValidateAsync_Should_Throw_ServiceException_If_PageSize_Greater_Than_50()
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