using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPlantao.Application.Services.Setor;
using System.Threading.Tasks;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetorController : ControllerBase
{
    private readonly SetorService _service;

    public SetorController(SetorService service)
    {
        _service = service;
    }


    [HttpGet("setores")]
    public async Task<IActionResult> GetSetores()
    {
        var responce = await _service.Consultar();
        return Ok(responce);
    }

    [HttpGet("setores/{id}")]
    public async Task<IActionResult> GetSetorId(long id)
    {
        var responce = await _service.ConsultarId(id);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest("Setor não existente ou não encontrado");
    }
    
    [HttpPost("setores")]
    public async Task<IActionResult> PostSetores([FromBody] SetorModel setor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Cadastrar(setor);
        if (response)
        {
            return Created("Setor adcionado", setor);
        }
        return BadRequest("Não foi possivel criar esse setor");
    }

    [HttpPut("setores")]
    public async Task<IActionResult> PutSetores([FromBody] SetorModel setor)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Editar(setor);
        if (response)
        {
            return Created("Setor editado", setor);
        }
        return BadRequest("Não foi possivel alterar esse setor");
    }

    [HttpDelete("setores/{id}")]
    public async Task<IActionResult> DeleteSetores(long id)
    {
        var responce = await _service.Deletar(id);
        if (responce != null)
        {
            return Accepted("Setor deletado com sucesso", responce);
        }
        return BadRequest("Não foi possivel deletar esse setor");
    }
    
}