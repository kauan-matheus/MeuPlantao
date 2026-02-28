using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Controllers;

public class ProfissionaisController
{
    private readonly AppDbContext _appDbContext;

    public ProfissionaisController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("profissionais")]
    public async Task<IActionResult> GetProfissionais()
    {
        var resultado = await _appDbContext.Profissionais.ToListAsync();
        return Ok(resultado);
    }
    
}