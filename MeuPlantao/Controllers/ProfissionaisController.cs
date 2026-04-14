using MeuPlantao.Application.Services.Profissional;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Todos os endpoints exigem autenticação por padrão
public class ProfissionaisController : ControllerBase
{
    // Injeta a interface, não a classe concreta — segue Clean Architecture
    private readonly IProfissionalService _service;

    public ProfissionaisController(IProfissionalService service)
    {
        _service = service;
    }

    [HttpGet("profissionais")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfissionais()
    {
        var response = await _service.Consultar();
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpGet("profissionais/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetProfissionalId(long id)
    {
        var response = await _service.ConsultarId(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }
    [HttpGet("profissionais/user{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetProfissionalIUserd(long id)
    {
        var response = await _service.ConsultarUserId(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("profissionais")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostProfissional([FromBody] RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Cadastrar(profissional);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPut("profissionais")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Edição restrita ao admin
    [ProducesResponseType(StatusCodes.Status200OK)] // PUT bem-sucedido retorna 200, não 201
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutProfissionais([FromBody] RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Editar(profissional);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpDelete("profissionais/{id}")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Exclusão restrita ao admin
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeleteProfissionais(long id)
    {
        var response = await _service.Deletar(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }
}