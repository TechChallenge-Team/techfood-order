using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Domain.Events.Preparation;
using TechFood.Domain.Repositories;

namespace TechFood.Application.Orders.Events;

internal class UpdateOrderOnPreparationReadyHandler(IOrderRepository repo) : INotificationHandler<PreparationReadyEvent>
{
    public async Task Handle(PreparationReadyEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
        {
            //throw new Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
        }

        order.Ready();
    }
}
