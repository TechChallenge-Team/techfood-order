using TechFood.Order.Application.Dto;
using TechFood.Order.Application.Queries;
using TechFood.Order.Application.Queries.GetById;
using TechFood.Shared.Domain.Enums;

namespace TechFood.Order.Application.Tests.Queries;

public class GetOrderByIdQueryHandlerTests
{
    private readonly Mock<IOrderQueryProvider> _queryProviderMock;
    private readonly GetOrderByIdQueryHandler _handler;

    public GetOrderByIdQueryHandlerTests()
    {
        _queryProviderMock = new Mock<IOrderQueryProvider>();
        _handler = new GetOrderByIdQueryHandler(_queryProviderMock.Object);
    }

    [Fact(DisplayName = "Should return order when found by Id")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithExistingOrderId_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var orderItems = new List<OrderItemDto>
        {
            new OrderItemDto(
                productId,
                "Big Mac",
                "https://example.com/bigmac.jpg",
                25.50m,
                2)
        };

        var expectedOrder = new OrderDto(
            orderId,
            123,
            51.00m,
            DateTime.Now,
            customerId,
            OrderStatusType.Received,
            orderItems);

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(orderId);
        result.Number.Should().Be(123);
        result.Amount.Should().Be(51.00m);
        result.CustomerId.Should().Be(customerId);
        result.Status.Should().Be(OrderStatusType.Received);
        result.Items.Should().HaveCount(1);
        result.Items[0].Name.Should().Be("Big Mac");
        result.Items[0].Quantity.Should().Be(2);

        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should return null when order not found")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithNonExistingOrderId_ShouldReturnNull()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync((OrderDto?)null);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should return order without customer")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithOrderWithoutCustomer_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        var orderItems = new List<OrderItemDto>
        {
            new OrderItemDto(
                Guid.NewGuid(),
                "Cheeseburger",
                null,
                15.00m,
                1)
        };

        var expectedOrder = new OrderDto(
            orderId,
            456,
            15.00m,
            DateTime.Now,
            null, // No customer
            OrderStatusType.Pending,
            orderItems);

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.CustomerId.Should().BeNull();
        result.Id.Should().Be(orderId);
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should return order with multiple items")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithOrderWithMultipleItems_ShouldReturnOrder()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        var orderItems = new List<OrderItemDto>
        {
            new OrderItemDto(Guid.NewGuid(), "Big Mac", "img1.jpg", 25.50m, 2),
            new OrderItemDto(Guid.NewGuid(), "French Fries", "img2.jpg", 10.00m, 3),
            new OrderItemDto(Guid.NewGuid(), "Coca-Cola", null, 8.00m, 2)
        };

        var expectedOrder = new OrderDto(
            orderId,
            789,
            87.00m,
            DateTime.Now,
            Guid.NewGuid(),
            OrderStatusType.InPreparation,
            orderItems);

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Items.Should().HaveCount(3);
        result.Items[0].Name.Should().Be("Big Mac");
        result.Items[1].Name.Should().Be("French Fries");
        result.Items[2].Name.Should().Be("Coca-Cola");
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should return order with empty items list")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithOrderWithNoItems_ShouldReturnOrderWithEmptyList()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        var expectedOrder = new OrderDto(
            orderId,
            100,
            0m,
            DateTime.Now,
            Guid.NewGuid(),
            OrderStatusType.Pending,
            new List<OrderItemDto>());

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Items.Should().BeEmpty();
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Theory(DisplayName = "Should return order with different statuses")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    [InlineData(OrderStatusType.Pending)]
    [InlineData(OrderStatusType.Received)]
    [InlineData(OrderStatusType.InPreparation)]
    [InlineData(OrderStatusType.Ready)]
    [InlineData(OrderStatusType.Delivered)]
    public async Task Handle_WithDifferentOrderStatuses_ShouldReturnOrderWithCorrectStatus(OrderStatusType status)
    {
        // Arrange
        var orderId = Guid.NewGuid();

        var expectedOrder = new OrderDto(
            orderId,
            999,
            100.00m,
            DateTime.Now,
            Guid.NewGuid(),
            status,
            new List<OrderItemDto>());

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Status.Should().Be(status);
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should handle empty Guid")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithEmptyGuid_ShouldCallQueryProvider()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(emptyGuid))
            .ReturnsAsync((OrderDto?)null);

        var query = new GetOrderByIdQuery(emptyGuid);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _queryProviderMock.Verify(x => x.GetOrderByIdAsync(emptyGuid), Times.Once);
    }

    [Fact(DisplayName = "Should handle cancellation token")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_WithCancellationToken_ShouldExecuteNormally()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();

        var expectedOrder = new OrderDto(
            orderId,
            555,
            50.00m,
            DateTime.Now,
            null,
            OrderStatusType.Pending,
            new List<OrderItemDto>());

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(orderId))
            .ReturnsAsync(expectedOrder);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(orderId);
    }

    [Fact(DisplayName = "Should verify query Id is passed correctly")]
    [Trait("Application", "GetOrderByIdQueryHandler")]
    public async Task Handle_ShouldPassCorrectIdToQueryProvider()
    {
        // Arrange
        var orderId = Guid.NewGuid();

        _queryProviderMock
            .Setup(x => x.GetOrderByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((OrderDto?)null);

        var query = new GetOrderByIdQuery(orderId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _queryProviderMock.Verify(
            x => x.GetOrderByIdAsync(It.Is<Guid>(id => id == orderId)),
            Times.Once);
    }
}
