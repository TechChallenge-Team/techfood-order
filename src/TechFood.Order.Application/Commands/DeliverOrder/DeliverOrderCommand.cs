using System;
using MediatR;

namespace TechFood.Order.Application.Commands.DeliverOrder;

public record DeliverOrderCommand(Guid Id) : IRequest<Unit>;
