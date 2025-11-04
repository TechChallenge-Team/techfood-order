using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Order.Domain.Entities;

namespace TechFood.Order.Domain.Repositories;

public interface IProductRepository
{
    Task<Guid> AddAsync(Product product);

    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);
}
