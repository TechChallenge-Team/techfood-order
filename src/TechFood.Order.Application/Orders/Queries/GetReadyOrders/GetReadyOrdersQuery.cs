using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Orders.Dto;

namespace TechFood.Order.Application.Orders.Queries.GetReadyOrders;

public class GetReadyOrdersQuery : IRequest<List<OrderDto>>;
