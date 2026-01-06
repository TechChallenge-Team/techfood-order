using MediatR;
using TechFood.Order.Application.Events.Integration.Incoming;
using TechFood.Order.Application.Events.Integration.Incoming.Handlers;
using TechFood.Order.Domain.Repositories;
using TechFood.Shared.Domain.Enums;

namespace TechFood.Order.Application.Tests.Events;

public class UpdateOrderOnPaymentConfirmedHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UpdateOrderOnPaymentConfirmedHandler _handler;

    public UpdateOrderOnPaymentConfirmedHandlerTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new UpdateOrderOnPaymentConfirmedHandler(_orderRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact(DisplayName = "Should update order to received status when payment is confirmed")]
    [Trait("Application", "UpdateOrderOnPaymentConfirmedHandler")]
    public async Task Handle_WithValidOrder_ShouldUpdateStatusToReceived()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Domain.Entities.Order(123, Guid.NewGuid());
        var notification = new PaymentConfirmedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await _handler.Handle(notification, CancellationToken.None);

        // Assert
        order.Status.Should().Be(OrderStatusType.Received);
        _orderRepositoryMock.Verify(x => x.GetByIdAsync(orderId), Times.Once);
    }

    [Fact(DisplayName = "Should throw exception when order not found")]
    [Trait("Application", "UpdateOrderOnPaymentConfirmedHandler")]
    public async Task Handle_WithNonExistentOrder_ShouldThrowException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var notification = new PaymentConfirmedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync((Domain.Entities.Order?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Shared.Application.Exceptions.ApplicationException>(
            async () => await _handler.Handle(notification, CancellationToken.None));

        exception.Message.Should().Contain("not found");
    }

    [Fact(DisplayName = "Should throw exception when order is not in pending status")]
    [Trait("Application", "UpdateOrderOnPaymentConfirmedHandler")]
    public async Task Handle_WithNonPendingOrder_ShouldThrowException()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Domain.Entities.Order(123, Guid.NewGuid());
        order.Receive(); // Change status to Received

        var notification = new PaymentConfirmedEvent(orderId);

        _orderRepositoryMock
            .Setup(x => x.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<Shared.Domain.Exceptions.DomainException>(
            async () => await _handler.Handle(notification, CancellationToken.None));
    }
}
