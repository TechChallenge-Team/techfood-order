using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Order.Domain.Repositories;
using TechFood.Order.Infra.Persistence.Contexts;

namespace TechFood.Order.Infra.Persistence.Repositories;

internal class OrderRepository(OrderContext dbContext) : IOrderRepository
{
    private readonly DbSet<Domain.Entities.Order> _orders = dbContext.Orders;

    public async Task<Guid> AddAsync(Domain.Entities.Order order)
    {
        var entry = await _orders.AddAsync(order);

        return entry.Entity.Id;
    }

    public async Task<Domain.Entities.Order?> GetByIdAsync(Guid id)
    {
        var t = await _orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        return t;
    }
}
