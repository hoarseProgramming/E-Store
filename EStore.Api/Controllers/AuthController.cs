using System.Security.Claims;
using EStore.Api.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Api.Controllers;

public class AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, EStoreContext eStoreContext) : ControllerBase
{
    [HttpGet(ApiEndpoints.Auth.Get)]
    public async Task<IActionResult> GetRoles()
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
    
    [HttpGet(ApiEndpoints.Auth.RegisterAdmin)]
    public async Task<IActionResult> RegisterAsAdmin()
    {
        var user = new IdentityUser() {UserName = "TestAdmin", Email = "test@admin.com"};
        var result = await userManager.CreateAsync(user, "TestAdmin123!");

        if (!result.Succeeded)
        {
            return BadRequest(new {message = "Could not create user for whatever reason"});
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