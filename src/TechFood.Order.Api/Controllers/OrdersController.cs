using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechFood.Order.Application.Commands.CreateOrder;
using TechFood.Order.Application.Commands.DeliverOrder;
using TechFood.Order.Application.Queries.GetById;
using TechFood.Order.Application.Queries.GetOrders;
using TechFood.Order.Application.Queries.GetReadyOrders;
using TechFood.Order.Contracts.Orders;

namespace TechFood.Order.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
[Authorize]
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Policy = "orders.write")]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(
            request.CustomerId,
            request.Items.ConvertAll(
                item => new CreateOrderCommand.Item(
                    item.ProductId,
                    item.Quantity)));

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet]
    [Authorize(Policy = "orders.read")]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = new GetOrdersQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetOrderByIdQuery(id);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("ready")]
    [Authorize(Policy = "orders.read")]
    public async Task<IActionResult> GetReadyAsync()
    {
        var query = new GetReadyOrdersQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPatch]
    [Route("{id:guid}/deliver")]
    [Authorize(Policy = "orders.write")]
    public async Task<IActionResult> DeliverAsync(Guid id)
    {
        var command = new DeliverOrderCommand(id);

        await _mediator.Send(command);

        return Ok();
    }
}
