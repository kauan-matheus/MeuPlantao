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

        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpGet("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetPlantaoId(long id)
    {

        var response = await _service.ConsultarId(id);

        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
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
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPut("plantoes")]
    [ProducesResponseType(StatusCodes.Status200OK)] // PUT bem-sucedido retorna 200, não 201
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutPlantoes([FromBody] RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserId();

        var response = await _service.Editar(plantao, userId);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpDelete("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeletePlantoes(long id)
    {
        var response = await _service.Deletar(id);
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
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
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
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
        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
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