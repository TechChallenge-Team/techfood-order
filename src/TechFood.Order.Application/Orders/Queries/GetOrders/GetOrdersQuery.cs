using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Orders.Dto;

namespace TechFood.Order.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery : IRequest<List<OrderDto>>;
