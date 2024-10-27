using IDsas.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDsas.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userUserService)
    {
        _userService = userUserService;
    }

    [HttpGet("login")]
    public IActionResult Login(string identityToken)
    {
        return Ok(identityToken);
    }
}