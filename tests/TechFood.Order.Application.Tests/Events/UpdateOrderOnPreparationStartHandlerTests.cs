using TechFood.Order.Application.Events.Handlers;
using TechFood.Order.Domain.Repositories;
using TechFood.Shared.Domain.Enums;

namespace TechFood.Order.Application.Tests.Events;

public class UpdateOrderOnPreparationStartHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly UpdateOrderOnPreparationStartHandler _handler;

    public UpdateOrderOnPreparationStartHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _handler = new UpdateOrderOnPreparationStartHandler(_orderRepositoryMock.Object);
    }

    [Fact(DisplayName = "Should update order to in preparation status when preparation starts")]
    [Trait("Application", "UpdateOrderOnPreparationStartHandler")]
    public async Task Handle_WithReceivedOrder_ShouldUpdateStatusToInPreparation()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Domain.Entities.Order(123, Guid.NewGuid());
        order.Receive(); // Must be received first

        var notification = new PreparationStartedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await _handler.Handle(notification, CancellationToken.None);

        // Assert
        order.Status.Should().Be(OrderStatusType.InPreparation);
        _orderRepositoryMock.Verify(x => x.GetByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should throw exception when order not found")]
    [Trait("Application", "UpdateOrderOnPreparationStartHandler")]
    public async Task Handle_WithNonExistentOrder_ShouldThrowException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var notification = new PreparationStartedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync((Domain.Entities.Order?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Shared.Application.Exceptions.ApplicationException>(
            async () => await _handler.Handle(notification, CancellationToken.None));

        exception.Message.Should().Contain("not found");
    }

    [Fact(DisplayName = "Should throw exception when order is not in received status")]
    [Trait("Application", "UpdateOrderOnPreparationStartHandler")]
    public async Task Handle_WithNonReceivedOrder_ShouldThrowException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Domain.Entities.Order(123, Guid.NewGuid());
        // Order is in Pending status

        var notification = new PreparationStartedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<Shared.Domain.Exceptions.DomainException>(
            async () => await _handler.Handle(notification, CancellationToken.None));
    }
}
