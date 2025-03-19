using EStore.Api.Mapping;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

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
            return NotFound();
        }

        var response = order.MapToResponse();

        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Orders.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var orders = await orderService.GetAllAsync();

        var response = orders.MapToResponse();

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Orders.Delete)]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        var deleted = await orderService.DeleteByIdAsync(id);

        if (!deleted)
        {
            return BadRequest(new { message = "Couldn't delete order" });
        }

        return Ok();
    }
}