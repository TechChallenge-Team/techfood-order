using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFood.Order.Application.Dto;
using TechFood.Order.Application.Queries.GetOrders;

namespace TechFood.Order.Api.Controllers
{
    [ApiController()]
    [Route("v1/[controller]")]
    [Authorize]
    public class ProductsController() : ControllerBase
    {

        [HttpGet]
        [Authorize(Policy = "products.read")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = new List<ProductDto>()
            {
                new ProductDto(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Pizza Margherita", "te", 10.50m),
                new ProductDto(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Spaghetti Carbonara", "te", 12.00m),
                new ProductDto(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Tiramisu", "te", 6.00m),
                new ProductDto(Guid.Parse("44444444-4444-4444-4444-444444444444"), "Lasagna", "te", 11.00m),
            };

            return Ok(result);
        }
    }
}
