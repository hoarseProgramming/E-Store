using EStore.Api.Mapping;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;
[Authorize]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost(ApiEndpoints.Orders.Create)]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var order = request.MapToOrder();

        var created = await orderService.CreateAsync(order);

        if (!created)
        {
            return BadRequest(new { message = "Couldn't create order" });
        }

        var response = order.MapToResponse();

        return CreatedAtAction(nameof(Get), new { Id = response.Id }, response);
    }

    [HttpGet(ApiEndpoints.Orders.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var order = await orderService.GetByIdAsync(id);

        if (order is null)
        {
            return NotFound(new { message = $"Order with id {id} not found." });
        }

        var response = order.MapToResponse();

        return Ok(response);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet(ApiEndpoints.Orders.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var orders = await orderService.GetAllAsync();

        var response = orders.MapToResponse();

        return Ok(response);
    }
}