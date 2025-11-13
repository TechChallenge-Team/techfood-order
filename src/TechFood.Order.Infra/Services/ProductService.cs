using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Order.Application.Common.Services.Interfaces;
using TechFood.Order.Application.Dto;

namespace TechFood.Order.Infra.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/v1/products", cancellationToken);

        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>(cancellationToken);

        return products ?? [];
    }
}
