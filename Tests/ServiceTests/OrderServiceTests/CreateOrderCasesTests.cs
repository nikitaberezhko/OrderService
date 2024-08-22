using AutoMapper;
using Domain;
using Moq;
using Services.Bus.Interfaces;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderServiceTests;

public class CreateOrderCasesTests
{
    private readonly IOrderService _orderService;
    
    public CreateOrderCasesTests()
    {
        var repositoryMock = new Mock<IOrderRepository>();
        repositoryMock.Setup(x => x.AddAsync(It.IsAny<Order>()))
            .ReturnsAsync(Guid.NewGuid());

        _orderService = new Services.Services.Implementations.OrderService(
            repositoryMock.Object,
            Provider.Get<IMapper>(),
            new Mock<ICreateOrderProducer>().Object,
            new Mock<IDeleteOrderProducer>().Object,
            new Mock<IUpdateOrderProducer>().Object,
            Provider.Get<OrderValidator>());
    }
    
    [Fact]
    public async Task CreateOrderCase_Should_Return_Not_Empty_Id()
    {
        // Arrange
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
        var actual = await _orderService.Create(model);

        // Assert
        Assert.NotEqual(Guid.Empty, actual);
    }
}