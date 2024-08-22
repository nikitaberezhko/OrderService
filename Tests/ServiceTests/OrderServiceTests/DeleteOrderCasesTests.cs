using AutoMapper;
using Domain;
using Moq;
using Services.Bus.Interfaces;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderServiceTests;

public class DeleteOrderCasesTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;

    public DeleteOrderCasesTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
    }

    [Fact]
    public async Task DeleteCase_Should_Return_OrderModel_With_Equal_Id()
    {
        // Arrange
        var model = new DeleteOrderModel { Id = Guid.NewGuid() };
        _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Order>()))
            .ReturnsAsync(CreateOrderWithEqualId(model));
        var service = new Services.Services.Implementations.OrderService(
            _repositoryMock.Object,
            Provider.Get<IMapper>(),
            new Mock<ICreateOrderProducer>().Object,
            new Mock<IDeleteOrderProducer>().Object,
            new Mock<IUpdateOrderProducer>().Object,
            Provider.Get<OrderValidator>());
        
        // Act
        var actual = await service.Delete(model);
        
        // Assert
        Assert.Equal(model.Id, actual.Id);
    }

    private Order CreateOrderWithEqualId(DeleteOrderModel model)
    {
        return new Order
        {
            Id = model.Id,
            ClientId = Guid.NewGuid(),
            DateStart = DateTime.Now - TimeSpan.FromDays(1),
            DateEnd = DateTime.Now + TimeSpan.FromDays(1),
            HubStartId = Guid.NewGuid(),
            HubEndId = Guid.NewGuid(),
            Price = 1000,
            Containers = new List<Container> 
                { new() { Id = Guid.NewGuid() } },
            DownTimes = new List<DownTime>()
        };
    }
}