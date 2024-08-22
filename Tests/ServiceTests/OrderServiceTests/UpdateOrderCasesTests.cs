using AutoMapper;
using Domain;
using Moq;
using Services.Bus.Interfaces;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderServiceTests;

public class UpdateOrderCasesTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    
    public UpdateOrderCasesTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
    }
    
    [Fact]
    public async Task UpdateOrderCase_Should_Return_OrderModel_With_Equal_Fields()
    {
        // Arrange
        var model = CreateModelForUpdate();
        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Order>()))
            .ReturnsAsync(ConvertOrderModelToOrder(model));
        var service = new Services.Services.Implementations.OrderService(
            _repositoryMock.Object,
            Provider.Get<IMapper>(),
            new Mock<ICreateOrderProducer>().Object,
            new Mock<IDeleteOrderProducer>().Object,
            new Mock<IUpdateOrderProducer>().Object,
            Provider.Get<OrderValidator>());
        
        // Act
        var actual = await service.Update(model);

        // Assert
        Assert.Equivalent(model, actual);
    }

    private Order ConvertOrderModelToOrder(UpdateOrderModel model)
    {
        return new Order
        {
            Id = model.Id,
            DateStart = model.DateStart,
            DateEnd = model.DateEnd,
            HubStartId = model.HubStartId,
            HubEndId = model.HubEndId,
            Price = model.Price,
            Containers = new List<Container> 
                { new() { Id = model.Containers.First().Id } },
            DownTimes = new List<DownTime>()
        };
    }

    private UpdateOrderModel CreateModelForUpdate()
    {
        return new UpdateOrderModel
        {
            Id = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<ContainerModel> 
                { new() { Id = Guid.NewGuid() } },
            DownTimes = new List<DownTimeModel>()
        };
    }
}