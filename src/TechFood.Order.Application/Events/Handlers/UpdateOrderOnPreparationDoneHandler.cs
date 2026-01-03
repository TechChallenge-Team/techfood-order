using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Resources;
using TechFood.Order.Domain.Repositories;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Events.Handlers;

public record PreparationDoneEvent(Guid OrderId) : IIntegrationEvent;

internal class UpdateOrderOnPreparationDoneHandler(IOrderRepository repo) : INotificationHandler<PreparationDoneEvent>
{
    public async Task Handle(PreparationDoneEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
            throw new Shared.Application.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);

        order.Ready();
    }
}
