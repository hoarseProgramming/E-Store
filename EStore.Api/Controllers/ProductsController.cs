using EStore.Api.Mapping;
using EStore.Api.Repositories;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
    {
        var product = request.MapToProduct();

        var created = await _productService.CreateAsync(product);

        if (!created)
        {
            return BadRequest();
        }
        
        var response = product.MapToResponse();

        return CreatedAtAction(nameof(Get), new { productNumber = product.ProductNumber }, response);
    }

    [HttpGet(ApiEndpoints.Products.Get)]
    public async Task<IActionResult> Get([FromRoute] int productNumber)
    {
        var product = await _productService.GetByProductNumberAsync(productNumber);

        if (product is null)
        {
            return NotFound();
        }

        var response = product.MapToResponse();
        
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Products.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        var response = products.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Products.Update)]
    public async Task<IActionResult> Update([FromRoute]int productNumber,
        [FromBody]UpdateProductRequest request)
    {
        var product = request.MapToProduct(productNumber);

        var updated = await _productService.UpdateAsync(product);

        if (!updated)
        {
            return NotFound();
        }

        var response = product.MapToResponse();

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Products.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int productNumber)
    {
        var deleted = await _productService.DeleteByProductNumberAsync(productNumber);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}