using EStore.Api.Mapping;
using EStore.Api.Repositories;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

[ApiController]
public class ProductsController(IProductRepository productRepository) : ControllerBase
{
    private readonly IProductRepository _productRepository = productRepository;

    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
    {
        var product = request.MapToProduct();

        await _productRepository.CreateAsync(product);

        var response = product.MapToResponse();

        return CreatedAtAction(nameof(Get), new { productNumber = product.ProductNumber }, response);
    }

    [HttpGet(ApiEndpoints.Products.Get)]
    public async Task<IActionResult> Get([FromRoute] int productNumber)
    {
        var product = await _productRepository.GetByProductNumberAsync(productNumber);

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
        var products = await _productRepository.GetAllAsync();

        var response = products.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Products.Update)]
    public async Task<IActionResult> Update([FromRoute]int productNumber,
        [FromBody]UpdateProductRequest request)
    {
        var product = request.MapToProduct(productNumber);

        var updated = await _productRepository.UpdateAsync(product);

        if (!updated)
        {
            return NotFound();
        }

        Console.WriteLine("");
        var response = product.MapToResponse();

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Products.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int productNumber)
    {
        var deleted = await _productRepository.DeleteByProductNumberAsync(productNumber);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}