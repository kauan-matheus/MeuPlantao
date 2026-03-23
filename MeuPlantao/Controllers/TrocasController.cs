using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MeuPlantao.Application.Services.TrocaPlantao;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Todos os endpoints exigem autenticação por padrão
public class TrocasController : ControllerBase
{
    // Injeta a interface, não a classe concreta — segue Clean Architecture
    private readonly ITrocaPlantaoService _service;

    public TrocasController(ITrocaPlantaoService service)
    {
        _service = service;
    }

    [HttpGet("trocas")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional))] // Leitura para ambos os roles
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrocas()
    {
        var response = await _service.Consultar();
        return Ok(response);
    }

    [HttpGet("trocas/{id}")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetTrocasId(long id)
    {
        var response = await _service.ConsultarId(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Troca não existente ou não encontrado");
    }

    [HttpPost("trocas")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostTrocas([FromBody] RequestTrocaPlantaoRegisterJson troca)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Cadastrar(troca, userId);
        if (response)
            return Created("Troca adicionada", troca);

        return BadRequest("Não foi possível criar essa troca");
    }

    [HttpPost("trocas/{id}/aceitar")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Aceitar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Aceitar(id, userId);
        if (response)
            return Ok("Troca aceita");

        return BadRequest("Não foi possível alterar essa troca");
    }

    [HttpPost("trocas/{id}/recusar")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Recusar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Recusar(id, userId);
        if (response)
            return Ok("Troca recusada");

        return BadRequest("Não foi possível alterar essa troca");
    }

    [HttpPost("trocas/{id}/enviar-aprovacao")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EnviarAprovacao(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.EnviarParaAprovacao(id, userId);
        if (response)
            return Ok("Troca enviada para aprovacao");

        return BadRequest("Não foi possível alterar essa troca");
    }

    [HttpPost("trocas/{id}/aprovar")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> aprovar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Aprovar(id, userId);
        if (response)
            return Ok("Troca aprovada");

        return BadRequest("Não foi possível alterar essa troca");
    }

    [HttpPost("trocas/{id}/reprovar")]
    [Authorize(Roles = nameof(RoleEnum.Admin) + "," + nameof(RoleEnum.Profissional) )] // Escrita restrita ao admin
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Reprovar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Reprovar(id, userId);
        if (response)
            return Ok("Troca reprovada");

        return BadRequest("Não foi possível alterar essa troca");
    }

    [HttpDelete("trocas/{id}")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Exclusão restrita ao admin
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeleteTrocas(long id)
    {
        var response = await _service.Deletar(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Não foi possível deletar essa troca");
    }

    private long GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("Usuário não autenticado");

        if (!long.TryParse(userId, out var id))
            throw new Exception("Id do usuário inválido no token");

        return id;
    }
}