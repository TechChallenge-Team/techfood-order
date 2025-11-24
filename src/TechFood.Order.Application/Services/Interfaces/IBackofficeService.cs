using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Services.Interfaces;

public interface IBackofficeService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);
}
