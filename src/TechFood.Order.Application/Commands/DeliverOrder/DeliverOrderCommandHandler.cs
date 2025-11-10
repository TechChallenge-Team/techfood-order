using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Common.Resources;
using TechFood.Order.Domain.Repositories;

namespace TechFood.Order.Application.Commands.DeliverOrder;

public class DeliverOrderCommandHandler(IOrderRepository repo)
    : IRequestHandler<DeliverOrderCommand, Unit>
{
    public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(request.Id);

        if (order == null)
        {
            throw new Shared.Application.Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
        }

        order.Deliver();

        return Unit.Value;
    }
}
