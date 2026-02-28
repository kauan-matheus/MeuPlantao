using MeuPlantao.Data;
using MeuPlantao.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetorController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public SetorController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    [HttpGet("setores")]
    public async Task<IActionResult> GetSetores()
    {
        var resultado = await _appDbContext.Setores.ToListAsync();
        return Ok(resultado);
    }
    
    [HttpPost("setores")]
    public async Task<IActionResult> PostSetores([FromBody] SetorModel setor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _appDbContext.Setores.Add(setor);
        await _appDbContext.SaveChangesAsync();

        return Created("setor adcionado", setor);
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