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

        return NotFound("Plantão não existente ou não encontrado");
    }

    [HttpPost("plantoes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPlantao([FromBody] RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Cadastrar(plantao);
        if (response)
            return Created("Plantão adicionado", plantao);

        return BadRequest("Não foi possível criar esse plantão");
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

        return BadRequest("Não foi possível alterar esse plantão");
    }

    [HttpDelete("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeletePlantoes(long id)
    {
        var response = await _service.Deletar(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Não foi possível deletar esse plantão");
    }
}