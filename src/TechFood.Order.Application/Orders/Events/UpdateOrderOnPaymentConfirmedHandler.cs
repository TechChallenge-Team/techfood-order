using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Common.Resources;
using TechFood.Order.Domain.Repositories;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Orders.Events;

public record PaymentConfirmedEvent(Guid OrderId) : IIntegrationEvent;

internal class UpdateOrderOnPaymentConfirmedHandler(IOrderRepository repo) : INotificationHandler<PaymentConfirmedEvent>
{
    public async Task Handle(PaymentConfirmedEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
        {
            throw new Shared.Application.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
        }

        order.Receive();
    }
}
