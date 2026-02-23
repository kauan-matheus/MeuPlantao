using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]/")]

public class AuthController : ControllerBase
{
    [HttpPost("/auth/login")]
    public IActionResult Login()
    {
        return Ok();
    }
    
    [HttpPost("/auth/register")]
    public IActionResult Register()
    {
        return Ok();
    }
    
}