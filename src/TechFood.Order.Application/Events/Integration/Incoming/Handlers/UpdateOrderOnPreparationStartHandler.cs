using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Resources;
using TechFood.Order.Domain.Repositories;

namespace TechFood.Order.Application.Events.Integration.Incoming.Handlers;

internal class UpdateOrderOnPreparationStartHandler(IOrderRepository repo) : INotificationHandler<PreparationStartedEvent>
{
    public async Task Handle(PreparationStartedEvent notification, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(notification.OrderId);

        if (order == null)
            throw new Shared.Application.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
        
        order.Prepare();
    }
}
