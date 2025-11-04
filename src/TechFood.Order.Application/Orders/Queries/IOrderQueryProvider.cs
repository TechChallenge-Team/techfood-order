using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Order.Application.Orders.Dto;

namespace TechFood.Order.Application.Orders.Queries;

public interface IOrderQueryProvider
{
    Task<List<OrderDto>> GetOrdersAsync();

    Task<List<OrderDto>> GetReadyOrdersAsync();
}
