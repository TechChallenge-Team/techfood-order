using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Queries;

public interface IOrderQueryProvider
{
    Task<List<OrderDto>> GetOrdersAsync();

    Task<List<OrderDto>> GetReadyOrdersAsync();
}
