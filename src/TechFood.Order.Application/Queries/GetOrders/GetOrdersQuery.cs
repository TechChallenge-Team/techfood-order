using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Queries.GetOrders;

public record GetOrdersQuery : IRequest<List<OrderDto>>;
