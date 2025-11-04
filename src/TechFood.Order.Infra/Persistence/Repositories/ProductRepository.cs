using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Order.Domain.Entities;
using TechFood.Order.Domain.Repositories;
using TechFood.Order.Infra.Persistence.Contexts;

namespace TechFood.Order.Infra.Persistence.Repositories;

internal class ProductRepository(OrderContext dbContext) : IProductRepository
{
    private readonly DbSet<Product> _products = dbContext.Products;

    public async Task<Guid> AddAsync(Product order)
    {
        var entry = await _products.AddAsync(order);

        return entry.Entity.Id;
    }

    public Task<List<Product>> GetAllAsync()
    {
        return _products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var t = await _products
            .FirstOrDefaultAsync(o => o.Id == id);

        return t;
    }
}
