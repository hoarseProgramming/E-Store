using EStore.Api.Mapping;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;


[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [Authorize(Roles = "ADMIN")]
    [HttpPost(ApiEndpoints.Categories.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        var category = request.MapToCategory();

        var created = await _categoryService.CreateAsync(category);

        if (!created)
        {
            return BadRequest(new { message = $"Category with name {category.CategoryName} already exists." });
        }

        var response = category.MapToResponse();

        return CreatedAtAction(nameof(Get), new { Id = category.Id }, response);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound(new { message = $"Category with id {id} not found." });
        }

        var response = category.MapToResponse();

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet(ApiEndpoints.Categories.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        var response = categories.MapToResponse();

        return Ok(response);
    }
}