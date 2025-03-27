using EStore.Api.Database;
using EStore.Api.Models;
using EStore.Contracts.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EStore.Api.Controllers;

public class AuthController(UserManager<AuthUser> userManager, RoleManager<IdentityRole> roleManager, EStoreContext eStoreContext) : ControllerBase
{
    [HttpGet(ApiEndpoints.Auth.Get)]
    public IActionResult GetRoles()
    {
        var user = HttpContext.User;
        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType)
                .Select(c => new
                {
                    c.Value
                });

            return Ok(roles);
        }

        return Unauthorized();
    }

    //[HttpGet(ApiEndpoints.Auth.GetUserIdByEmail)]
    //public async Task<IActionResult> GetUserIdByEmail([FromRoute] string email)
    //{
    //    AuthUser? user = (AuthUser?)await userManager.FindByEmailAsync(email);

    //    if (user is null)
    //    {
    //        return NotFound();
    //    }

    //    var response = new UserIdResponse()
    //    {
    //        Id = Guid.Parse(user.Id)
    //    };

    //    return Ok(response);
    //}

    [HttpPost(ApiEndpoints.Auth.SetCustomerId)]
    public async Task<IActionResult> SetCustomerId([FromBody] SetCustomerRequest request)
    {
        AuthUser? user = (AuthUser?)await userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            user.CustomerId = request.CustomerId;

            await userManager.UpdateAsync(user);

            await eStoreContext.SaveChangesAsync();

            return Ok();
        }

        return NotFound();
    }

    [HttpGet(ApiEndpoints.Auth.RegisterAdmin)]
    public async Task<IActionResult> RegisterAsAdmin()
    {
        var user = new AuthUser() { UserName = "TestAdmin", Email = "test@admin.com" };
        var result = await userManager.CreateAsync(user, "TestAdmin123!");

        if (!result.Succeeded)
        {
            return BadRequest(new { message = "Could not create user for whatever reason" });
        }

        var roleExists = await roleManager.RoleExistsAsync("ADMIN");

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("ADMIN"));
        }

        await userManager.AddToRoleAsync(user, "ADMIN");

        await eStoreContext.SaveChangesAsync();

        return Ok();
    }
}