using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.Services.OrderValidatorTests;

public class UpdateOrderValidationTests
{
    [Fact]
    public async Task UpdateOrder_MustBeValid()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        var actual = await validator.ValidateAsync(model);
        
        // Assert
        Assert.True(actual);
    }

    [Fact]
    public async Task UpdateOrder_MustThrowBecauseIdIsInvalid()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.Empty,
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task UpdateOrder_MustThrowBecausePriceLessThan1()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 0,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task UpdateOrder_MustThrowBecauseDateStartLessThanDateEnd()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now + TimeSpan.FromDays(1),
            DateEnd = DateTime.Now - TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () =>
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task UpdateOrder_MustThrowBecauseHubStartIdIsInvalid()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.Empty,
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () =>
            await validator.ValidateAsync(model));
    }

    [Fact]
    public async Task UpdateOrder_MustThrowBecauseHubEndIdIsInvalid()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.Empty,
            Price = 1000,
            Containers = new List<ContainerModel>
            {
                new() { Id = Guid.NewGuid() }
            },
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () =>
            await validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task UpdateOrder_MustThrowBecauseContainersCollectionIsEmpty()
    {
        // Arrange
        var validator = CreateValidatorForUpdateCase();
        var model = new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel>(),
            DownTimes = new List<DownTimeModel>()
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await validator.ValidateAsync(model));
    }

    private OrderValidator CreateValidatorForUpdateCase() => 
        new(new Mock<IValidator<CreateOrderModel>>().Object,
            Provider.Get<IValidator<UpdateOrderModel>>(),
            new Mock<IValidator<DeleteOrderModel>>().Object,
            new Mock<IValidator<GetOrderByIdModel>>().Object,
            new Mock<IValidator<GetOrdersByClientIdModel>>().Object,
            new Mock<IValidator<GetOrdersInPeriodModel>>().Object,
            new Mock<IValidator<GetAllOrdersModel>>().Object);
}