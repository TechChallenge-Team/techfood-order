using System;
using MediatR;

namespace TechFood.Order.Application.Orders.Commands.DeliverOrder;

public record DeliverOrderCommand(Guid Id) : IRequest<Unit>;
