using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Domain.Repositories;
using TechFood.Shared.Domain.Resources;

namespace TechFood.Application.Orders.Commands.DeliverOrder;

public class DeliverOrderCommandHandler(IOrderRepository repo)
    : IRequestHandler<DeliverOrderCommand, Unit>
{
    public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repo.GetByIdAsync(request.Id);

        if (order == null)
        {
            //throw new Exceptions.ApplicationException(Exceptions.Order_OrderNotFound);
        }

        order.Deliver();

        return Unit.Value;
    }
}
