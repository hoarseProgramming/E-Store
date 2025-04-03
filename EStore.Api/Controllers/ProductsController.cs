using EStore.Api.Mapping;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

[Authorize]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost(ApiEndpoints.Products.Create)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var product = request.MapToProduct();

        var created = await _productService.CreateAsync(product);

        if (!created)
        {
            return BadRequest(new { message = "Couldn't create Product" });
        }

        var response = product.MapToResponse();

        return CreatedAtAction(nameof(Get), new { productNumber = product.ProductNumber }, response);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Products.Get)]
    public async Task<IActionResult> Get([FromRoute] int productNumber)
    {
        var product = await _productService.GetByProductNumberAsync(productNumber);

        if (product is null)
        {
            return NotFound(new { message = $"Product with number {productNumber} not found." });
        }

        var response = product.MapToResponse();

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Products.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();

        var response = products.MapToResponse();

        return Ok(response);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPut(ApiEndpoints.Products.Update)]
    public async Task<IActionResult> Update([FromRoute] int productNumber,
        [FromBody] UpdateProductRequest request)
    {
        var product = request.MapToProduct(productNumber);

        var updated = await _productService.UpdateAsync(product);

        if (!updated)
        {
            return NotFound(new { message = $"Product with number {productNumber} not found." });
        }

        var response = product.MapToResponse();

        return Ok(response);
    }
}