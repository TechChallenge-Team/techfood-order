using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Queries.GetById;

public class GetOrderByIdQueryHandler(IOrderQueryProvider queries) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        => await queries.GetOrderByIdAsync(request.Id);
}
