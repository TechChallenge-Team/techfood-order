using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Application.Common.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);
}
