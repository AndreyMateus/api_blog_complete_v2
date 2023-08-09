using Microsoft.AspNetCore.Mvc;
using blog.Services;
using Blog.Models;

namespace blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/login")]
    public IActionResult Login([FromServices]TokenService tokenService, [FromBody]User user)
    {   
        // var tokenService = new TokenService();

        var token = tokenService.GenerateToken(user);

        return Ok(token);
    }
}
