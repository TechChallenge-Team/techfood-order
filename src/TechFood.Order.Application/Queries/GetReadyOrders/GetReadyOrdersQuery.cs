using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Queries.GetReadyOrders;

public class GetReadyOrdersQuery : IRequest<List<OrderDto>>;
