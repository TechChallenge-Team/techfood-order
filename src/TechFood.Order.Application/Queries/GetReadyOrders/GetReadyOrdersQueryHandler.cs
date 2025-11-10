using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Dto;
using TechFood.Order.Application.Queries;

namespace TechFood.Order.Application.Queries.GetReadyOrders;

public class GetReadyOrdersQueryHandler(IOrderQueryProvider queries) : IRequestHandler<GetReadyOrdersQuery, List<OrderDto>>
{
    public Task<List<OrderDto>> Handle(GetReadyOrdersQuery request, CancellationToken cancellationToken)
        => queries.GetReadyOrdersAsync();
}
