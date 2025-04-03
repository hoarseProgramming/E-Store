using EStore.Api.Models;
using EStore.Api.Services;
using EStore.Contracts.Requests;
using EStore.Contracts.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EStore.Api.Controllers;

[Authorize]
public class UsersController(UserManager<AuthUser> userManager, IUserService userService, ICustomerService customerService) : ControllerBase
{
    [HttpGet(ApiEndpoints.Auth.Get)]
    public async Task<IActionResult> GetUserInfo()
    {
        var user = HttpContext.User;
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType)
                .Select(c => new Role(c.Value));

            AuthUser? authUser = (AuthUser?)await userManager.FindByNameAsync(user.Identity.Name);

            var response = new UserInfoResponse()
            {
                Id = Guid.Parse(authUser.Id),
                CustomerId = authUser.CustomerId,
                Roles = roles
            };

            return Ok(response);
        }

        return Unauthorized();
    }

    [HttpPost(ApiEndpoints.Auth.SetCustomerId)]
    public async Task<IActionResult> SetCustomerId([FromBody] SetCustomerRequest request)
    {
        AuthUser? user = (AuthUser?)await userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            user.CustomerId = request.CustomerId;

            var result = await userService.UpdateAsync(user);

            return result ? Ok() : BadRequest(new { message = $"Couldn't set customer Id." });
        }

        return NotFound(new { message = "User not found" });
    }

    [HttpPut(ApiEndpoints.Auth.UpdateUserAndCustomer)]
    public async Task<IActionResult> UpdateUserAndCustomer([FromBody] UpdateUserAndCustomerRequest request, [FromRoute] Guid id)
    {
        AuthUser? user = (AuthUser?)await userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound(new { message = "User not found." });
        }

        if (user.CustomerId is null)
        {
            return BadRequest(new { message = "Couldn't update info" });
        }

        user.UserName = request.Email;
        user.Email = request.Email;

        var userIsUpdated = await userService.UpdateAsync(user);

        if (!userIsUpdated)
        {
            return BadRequest(new { message = "Couldn't update info" });
        }

        var customer = new Customer()
        {
            Id = (Guid)user.CustomerId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Address = request.Address,
            ZipCode = request.ZipCode,
            City = request.City,
            Country = request.Country
        };

        var customerIsUpdated = await customerService.UpdateAsync(customer);

        return customerIsUpdated ? Ok() : NotFound(new { message = "User not found." });
    }
}