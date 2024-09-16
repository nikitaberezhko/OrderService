using AutoMapper;
using Domain;
using Moq;
using Services.Bus.Interfaces;
using Services.Models.Request;
using Services.Repositories.Interfaces;
using Services.Validation;
using Xunit;

namespace Tests.ServiceTests.OrderServiceTests;

public class GetByClientIdCasesTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;

    public GetByClientIdCasesTests()
    {
        _repositoryMock = new Mock<IOrderRepository>();
    }
    
    [Fact]
    public async Task GetByClientIdCase_Should_Return_Empty_List_If_Orders_Not_Found()
    {
        // Arrange
        var model = new GetOrdersByClientIdModel
        {
            ClientId = Guid.NewGuid()
        };
        _repositoryMock
            .Setup(x => x.GetByClientIdAsync(It.IsAny<Order>()))
            .ReturnsAsync(new List<Order>());
        var service = new Services.Services.Implementations.OrderService(
            _repositoryMock.Object,
            Provider.Get<IMapper>(),
            new Mock<ICreateOrderProducer>().Object,
            new Mock<IDeleteOrderProducer>().Object,
            new Mock<IUpdateOrderProducer>().Object,
            Provider.Get<OrderValidator>());
        
        // Act
        var actual = await service.GetByClientId(model);
        
        // Assert
        Assert.Empty(actual);
    }
}