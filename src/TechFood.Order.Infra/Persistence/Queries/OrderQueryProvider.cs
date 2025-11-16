using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Order.Application.Dto;
using TechFood.Order.Application.Queries;
using TechFood.Order.Infra.Persistence.Contexts;
using TechFood.Shared.Domain.Enums;

namespace TechFood.Order.Infra.Persistence.Queries;

internal class OrderQueryProvider(OrderContext techFoodContext) : IOrderQueryProvider
{
    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        //var products = await techFoodContext.Products.ToListAsync();

        var result = await techFoodContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .OrderBy(order => order.CreatedAt)
            .ToListAsync();

        return [.. result
            .Select(data => new OrderDto(
                data.Id,
                data.Number,
                data.Amount,
                data.CreatedAt,
                data.CustomerId,
                data.Status,
                [.. data.Items.Select(item =>
                {
                    //var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                    return new OrderItemDto(
                        item.Id,
                        null!,
                        null!,
                         //product!.Name,
                         //product.ImageUrl,
                        item.UnitPrice, item.Quantity);
                })]))];
    }

    public async Task<List<OrderDto>> GetReadyOrdersAsync()
    {
        //var products = await techFoodContext.Products.ToListAsync();

        var result = await techFoodContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Where(order => order.Status == OrderStatusType.Ready)
            .OrderBy(order => order.CreatedAt)
            .ToListAsync();

        return [.. result
             .Select(data => new OrderDto(
                 data.Id,
                 data.Number,
                 data.Amount,
                 data.CreatedAt,
                 data.CustomerId,
                 data.Status,
                 [.. data.Items.Select(item =>
                 {
                     //var product = products.FirstOrDefault(p => p.Id == item.ProductId);

                     return new OrderItemDto(
                         item.Id,
                         null!,
                         null!,
                         //product!.Name,
                         //product.ImageUrl,
                         item.UnitPrice, item.Quantity);
                 })]))];
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
    {
        var data = await techFoodContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
            .Where(order => order.Id == id)
            .FirstOrDefaultAsync();

        return data == null
            ? null
            : new OrderDto(
                 data.Id,
                 data.Number,
                 data.Amount,
                 data.CreatedAt,
                 data.CustomerId,
                 data.Status,
                 [.. data.Items.Select(item =>
                 {
                     return new OrderItemDto(
                         item.Id,
                         null!,
                         null!,
                         item.UnitPrice, item.Quantity);
                 })]);
    }
}
