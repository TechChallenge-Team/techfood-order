using System;
using System.Threading.Tasks;

namespace TechFood.Order.Domain.Repositories;

public interface IOrderRepository
{
    Task<Guid> AddAsync(Entities.Order order);

    Task<Entities.Order?> GetByIdAsync(Guid id);
}
