using System;
using MediatR;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Queries.GetById;

public class GetOrderByIdQuery : IRequest<OrderDto?>
{
    public GetOrderByIdQuery(Guid id)
        => Id = id;

    public Guid Id { get; set; }
}
