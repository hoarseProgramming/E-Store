﻿using EStore.Api.Mapping;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

[Authorize]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpPost(ApiEndpoints.Customers.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var customer = request.MapToCustomer();

        var created = await _customerService.CreateAsync(customer);

        if (!created)
        {
            return BadRequest(new { message = $"Customer with email {customer.Email} already exists." });
        }

        var response = customer.MapToResponse();

        return CreatedAtAction(nameof(Get), new { Id = response.Id }, response);
    }

    [HttpGet(ApiEndpoints.Customers.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var customer = await _customerService.GetByIdAsync(id);


        if (customer is null)
        {
            return NotFound(new { message = $"Customer with id {id} not found." });
        }

        var response = customer.MapToResponse();

        return Ok(response);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet(ApiEndpoints.Customers.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();

        var response = customers.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Customers.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id,
        [FromBody] UpdateCustomerRequest request)
    {
        var customer = request.MapToCustomer(id);

        var updated = await _customerService.UpdateAsync(customer);

        if (!updated)
        {
            return NotFound(new { message = $"Customer with id {id} not found." });
        }

        var response = customer.MapToResponse();

        return Ok(response);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete(ApiEndpoints.Customers.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _customerService.DeleteByIdAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = $"Customer with id {id} not found." });
        }

        return Ok();
    }
}