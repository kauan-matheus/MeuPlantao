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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrocas()
    {
        var response = await _service.Consultar();
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpGet("trocas/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetTrocasId(long id)
    {
        var response = await _service.ConsultarId(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostTrocas([FromBody] RequestTrocaPlantaoRegisterJson troca)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Cadastrar(troca, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas/{id}/aceitar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Aceitar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Aceitar(id, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas/{id}/recusar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Recusar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Recusar(id, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas/{id}/enviar-aprovacao")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EnviarAprovacao(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.EnviarParaAprovacao(id, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas/{id}/aprovar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> aprovar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Aprovar(id, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("trocas/{id}/reprovar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Reprovar(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Reprovar(id, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpDelete("trocas/{id}")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Exclusão restrita ao admin
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeleteTrocas(long id)
    {
        var response = await _service.Deletar(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
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