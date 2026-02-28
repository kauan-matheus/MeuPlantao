using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Data;
using Microsoft.EntityFrameworkCore;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Entities;

namespace MeuPlantao.Controllers;

public class ProfissionaisController : ControllerBase
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

    [HttpPost("profissionais")]
    public async Task<IActionResult> PostProfissional([FromBody]  RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var UserExiste = await _appDbContext.Usuarios
            .FindAsync(profissional.UserId);

        if (UserExiste == null)
            return BadRequest("Usuario não encontrado");

        var novo = new ProfissionalModel
        {
            Nome = profissional.Nome,
            Crm = profissional.Crm,
            Telefone = profissional.Telefone,
            UserId = profissional.UserId,
            User = UserExiste
        };

        _appDbContext.Profissionais.Add(novo);
        await _appDbContext.SaveChangesAsync();

        return Created("Profissional adcionado", profissional);
    }
    
}