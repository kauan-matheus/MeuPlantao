using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantaoController : ControllerBase
{
    [HttpGet("setores")]
    public IActionResult GetSetores()
    {
        return Ok();
    }
    
}