using System.Security.Claims;
using MeuPlantao.Application.Services.Plantao;
using MeuPlantao.Communication.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantaoController : ControllerBase
{
    // Injeta a interface, não a classe concreta — segue Clean Architecture
    private readonly IPlantaoService _service;

    public PlantaoController(IPlantaoService service)
    {
        _service = service;
    }

    [HttpGet("plantoes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlantoes()
    {
        var response = await _service.Consultar();
        return Ok(response);
    }

    [HttpGet("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetPlantaoId(long id)
    {
        var response = await _service.ConsultarId(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Plantao não existente ou não encontrado");
    }

    [HttpPost("plantoes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPlantao([FromBody] RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Cadastrar(plantao, userId);
        if (response)
            return Created("Plantao adicionado", plantao);

        return BadRequest("Nao foi possível criar esse plantao");
    }

    [HttpPut("plantoes")]
    [ProducesResponseType(StatusCodes.Status200OK)] // PUT bem-sucedido retorna 200, não 201
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutPlantoes([FromBody] RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Editar(plantao);
        if (response)
            return Ok(plantao);

        return BadRequest("Nao foi possivel alterar esse plantao");
    }

    [HttpDelete("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeletePlantoes(long id)
    {
        var response = await _service.Deletar(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Nao foi possivel deletar esse plantao");
    }

    [HttpPut("plantoes/{id}/solicitar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Solicitar(long id)
    {
       if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Solicitar(id, userId);
        if (response)
            return Ok("Solicitacao criada");

        return BadRequest("Nao foi possível executar essa solicitacao");
    }

    [HttpPut("plantoes/{id}/aceitar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AceitarSolicitacao(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.AceitarSolicitacao(id, userId);
        if (response)
            return Ok("Solicitacao aceita");

        return BadRequest("Nao foi possível aceitar essa solicitacao");
    }

    [HttpPut("plantoes/{id}/recusar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecusarSolicitacao(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.RecusarSolicitacao(id, userId);
        if (response)
            return Ok("Solicitacao recusada");

        return BadRequest("Nao foi possível recusar essa solicitacao");
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