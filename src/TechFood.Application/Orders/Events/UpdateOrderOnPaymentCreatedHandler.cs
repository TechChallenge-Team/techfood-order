using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Domain.Events.Payment;
using TechFood.Domain.Repositories;
using TechFood.Shared.Domain.Resources;

namespace TechFood.Application.Orders.Events;

internal class UpdateOrderOnPaymentCreatedHandler(IOrderRepository repo) : INotificationHandler<PaymentCreatedEvent>
{
    public async Task Handle(PaymentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
        {
            //throw new Exceptions.ApplicationException(Excekptions.Order_OrderNotFound);
        }

        order.Receive();
    }
}
