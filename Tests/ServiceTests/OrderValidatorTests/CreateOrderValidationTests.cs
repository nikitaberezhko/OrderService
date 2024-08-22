using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderValidatorTests;

public class CreateOrderValidationTests
{
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
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
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.Empty,
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
        };

        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_DateEnd_Less_Than_DateStart()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_If_HubStartId_Is_Invalid()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.Empty,
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_HubEndId_Is_Invalid()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.Empty,
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Price_Is_Zero()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 0,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            }
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Containers_Collection_Is_Empty()
    {
        // Arrange
        var validator = CreateValidatorForCreateCase();
        var model = new CreateOrderModel
        {
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    private OrderValidator CreateValidatorForCreateCase() =>
        new(Provider.Get<IValidator<CreateOrderModel>>(),
            new Mock<IValidator<UpdateOrderModel>>().Object,
            new Mock<IValidator<DeleteOrderModel>>().Object,
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}