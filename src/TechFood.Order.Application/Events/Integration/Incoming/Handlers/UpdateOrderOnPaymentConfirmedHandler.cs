using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Events.Integration.Outgoing;
using TechFood.Order.Application.Resources;
using TechFood.Order.Domain.Repositories;

namespace TechFood.Order.Application.Events.Integration.Incoming.Handlers;

internal class UpdateOrderOnPaymentConfirmedHandler(IOrderRepository repo, IMediator mediator)
    : INotificationHandler<PaymentConfirmedEvent>
{
    public async Task Handle(PaymentConfirmedEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
            throw new Shared.Application.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);

        order.Receive();

        await mediator.Publish(new OrderReceivedEvent(notification.OrderId), cancellationToken);
    }
}
