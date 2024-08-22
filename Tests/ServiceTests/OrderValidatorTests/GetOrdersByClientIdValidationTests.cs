using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderValidatorTests;

public class GetOrdersByClientIdValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateValidatorForGetByClientIdCase();
        var model = new GetOrdersByClientIdModel
        {
            ClientId = Guid.NewGuid()
        };
        
        // Act
        var actual = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(actual);
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_ClientId_Is_Invalid()
    {
        // Arrange
        var validator = CreateValidatorForGetByClientIdCase();
        var model = new GetOrdersByClientIdModel
        {
            ClientId = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    private OrderValidator CreateValidatorForGetByClientIdCase() =>
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            new Mock<IValidator<UpdateOrderModel>>().Object,
            new Mock<IValidator<DeleteOrderModel>>().Object,
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            Provider.Get<IValidator<GetOrdersByClientIdModel>>(),
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}