using System;
using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Commands.CreateOrder;

public record CreateOrderCommand(Guid? CustomerId, List<CreateOrderCommand.Item> Items) : IRequest<OrderDto>
{
    public record Item(Guid ProductId, int Quantity);
}
