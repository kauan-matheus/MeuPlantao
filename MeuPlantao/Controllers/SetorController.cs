using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetorController : ControllerBase
{
    [HttpGet("setores")]
    public IActionResult GetSetores()
    {
        return Ok();
    }
    
    [HttpPost("setores")]
    public IActionResult PostSetores()
    {
        return Ok();
    }

    [HttpPut("setores/{idSetores}")]
    public IActionResult PutSetores(int idSetores)
    {
        return Ok();
    }

    [HttpDelete("setores/{idSetores}")]
    public IActionResult DeleteSetores(int idSetores)
    {
        return Ok();
    }
    
}