using System;
using System.Collections.Generic;
using MediatR;
using TechFood.Order.Application.Orders.Dto;

namespace TechFood.Order.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid? CustomerId, List<CreateOrderCommand.Item> Items) : IRequest<OrderDto>
{
    public record Item(Guid ProductId, int Quantity);
}
