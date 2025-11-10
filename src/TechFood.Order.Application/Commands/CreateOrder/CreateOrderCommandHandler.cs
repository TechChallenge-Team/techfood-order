using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TechFood.Order.Application.Common.Services.Interfaces;
using TechFood.Order.Application.Dto;
using TechFood.Order.Domain.Entities;
using TechFood.Order.Domain.Repositories;

namespace TechFood.Order.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepo,
    //IProductRepository productRepo,
    IOrderNumberService orderNumberService
        ) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
        //var products = await productRepo.GetAllAsync();

        //var number = await orderNumberService.GetAsync();
        //var order = new Domain.Entities.Order(number, request.CustomerId);

        //var items = request.Items
        //    .Select(item =>
        //    {
        //        var product = products.First(p => p!.Id == item.ProductId)!;
        //        var orderItem = new OrderItem(product.Id, product.Price, item.Quantity);

        //        return (product, orderItem);
        //    })
        //    .ToList();

        //foreach (var (_, item) in items)
        //{
        //    order.AddItem(item);
        //}

        //await orderRepo.AddAsync(order);

        //return new OrderDto(
        //    order.Id,
        //    order.Number,
        //    order.Amount,
        //    order.CreatedAt,
        //    //deve criar o customer dto?
        //    request.CustomerId,
        //    order.Status,
        //    items.ConvertAll(item =>
        //        new OrderItemDto(
        //            item.orderItem.Id,
        //            item.product.Name,
        //            item.product.ImageUrl,
        //            item.product.Price,
        //            item.orderItem.Quantity
        //        )
        //    ));
    }
}
