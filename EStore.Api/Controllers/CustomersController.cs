using EStore.Api.Mapping;
using EStore.Api.Repositories;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;


[ApiController]
public class CustomersController(ICustomerRepository customerRepository) : ControllerBase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    [HttpPost(ApiEndpoints.Customers.Create)]
    public async Task<IActionResult> Create([FromBody]CreateCustomerRequest request)
    {
        var customer = request.MapToCustomer();

        await _customerRepository.CreateAsync(customer);

        var response = customer.MapToResponse();

        return CreatedAtAction(nameof(Get), new { Id = response.Id }, response);
    }

    [HttpGet(ApiEndpoints.Customers.Get)]
    public async Task<IActionResult> Get([FromRoute]Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        
        if (customer is null)
        {
            return NotFound();
        }
        
        var response = customer.MapToResponse();
        
        return Ok(response);
    }
    
    [HttpGet(ApiEndpoints.Customers.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerRepository.GetAllAsync();
    
        var response = customers.MapToResponse();
    
        return Ok(response);
    }
    
    [HttpPut(ApiEndpoints.Customers.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id,
        [FromBody]UpdateCustomerRequest request)
    {
        var customer = request.MapToCustomer(id);
    
        var updated = await _customerRepository.UpdateAsync(customer);
    
        if (!updated)
        {
            return NotFound();
        }
    
        var response = customer.MapToResponse();
    
        return Ok(response);
    }
    
    [HttpDelete(ApiEndpoints.Customers.Delete)]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        var deleted = await _customerRepository.DeleteByIdAsync(id);
    
        if (!deleted)
        {
            return NotFound();
        }
    
        return Ok();
    }
}