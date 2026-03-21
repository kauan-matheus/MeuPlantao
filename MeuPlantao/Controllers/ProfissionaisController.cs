using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Application.Services.Profissional;
using Microsoft.AspNetCore.Authorization;

namespace MeuPlantao.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfissionaisController : ControllerBase
{
    private readonly ProfissionalService _service;

    public ProfissionaisController(ProfissionalService service)
    {
        _service = service;
    }

    [HttpGet("profissionais")]
    // Leitura pode ser feita por profissional autenticado e admin.
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional))]
    public async Task<IActionResult> GetProfissionais()
    {
        var responce = await _service.Consultar();
        return Ok(responce);
    }

    [HttpGet("profissionais/{id}")]
    // Leitura pode ser feita por profissional autenticado e admin.
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional))]
    public async Task<IActionResult> GetProfissionalId(long id)
    {
        var responce = await _service.ConsultarId(id);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest("Profissional não existente ou não encontrado");
    }

    [HttpPost("profissionais")]
    // Escrita/gestão é restrita ao admin para evitar cadastro paralelo ao fluxo de auth público.
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public async Task<IActionResult> PostProfissional([FromBody]  RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Cadastrar(profissional);
        if (response)
        {
            return Created("Profissional adcionado", profissional);
        }
        return BadRequest("Não foi possivel criar esse profissional");
    }

    [HttpPut("profissionais")]
    // Escrita/gestão é restrita ao admin para evitar alteração indevida de perfil.
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public async Task<IActionResult> PutProfissionais([FromBody] RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Editar(profissional);
        if (response)
        {
            return Created("Profissional editado", profissional);
        }
        return BadRequest("Não foi possivel alterar esse profissional");
    }

    [HttpDelete("profissionais/{id}")]
    // Exclusão é operação administrativa.
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public async Task<IActionResult> DeleteProfissionais(long id)
    {
        var responce = await _service.Deletar(id);
        if (responce != null)
        {
            return Accepted("Profissional deletado com sucesso", responce);
        }
        return BadRequest("Não foi possivel deletar esse profissional");
    }
    
}