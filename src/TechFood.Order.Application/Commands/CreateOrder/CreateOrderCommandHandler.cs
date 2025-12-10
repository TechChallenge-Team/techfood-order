using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Dto;
using TechFood.Order.Application.Services.Interfaces;
using TechFood.Order.Domain.Entities;
using TechFood.Order.Domain.Repositories;
using TechFood.Shared.Application.Events;

namespace TechFood.Order.Application.Commands.CreateOrder;

public record OrderCreatedIntegrationEvent(
    Guid OrderId,
    List<OrderItemCreatedDto> Items
) : IIntegrationEvent;

public record OrderItemCreatedDto(
    Guid ProductId,
    string Name,
    decimal UnitPrice,
    int Quantity
);

public class CreateOrderCommandHandler(
    IOrderRepository orderRepo,
    IBackofficeService backofficeService,
    IOrderNumberService orderNumberService,
    IMediator mediator
        ) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var products = await backofficeService.GetProductsAsync(cancellationToken);

        var number = await orderNumberService.GetAsync();
        var order = new Domain.Entities.Order(number, request.CustomerId);

        var items = request.Items
            .Select(item =>
            {
                var product = products.First(p => p!.Id == item.ProductId)!;
                var orderItem = new OrderItem(
                    product.Id,
                    product.Price,
                    item.Quantity);

                return (product, orderItem);
            })
            .ToList();

        foreach (var (_, item) in items)
        {
            order.AddItem(item);
        }

        await orderRepo.AddAsync(order);

        await mediator.Publish(new OrderCreatedIntegrationEvent(
            order.Id,
            items.ConvertAll(item =>
                new OrderItemCreatedDto(
                    item.product.Id,
                    item.product.Name,
                    item.product.Price,
                    item.orderItem.Quantity
                )
            )), cancellationToken);

        return new OrderDto(
            order.Id,
            order.Number,
            order.Amount,
            order.CreatedAt,
            //deve criar o customer dto?
            request.CustomerId,
            order.Status,
            items.ConvertAll(item =>
                new OrderItemDto(
                    item.orderItem.Id,
                    item.product.Name,
                    item.product.ImageUrl,
                    item.product.Price,
                    item.orderItem.Quantity
                )
            ));
    }
}
