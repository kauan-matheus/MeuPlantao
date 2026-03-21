using MeuPlantao.Application.Services.Plantao;
using MeuPlantao.Communication.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantaoController : ControllerBase
{
    private readonly PlantaoService _service;

    public PlantaoController(PlantaoService service)
    {
        _service = service;
    }

    [HttpGet("plantoes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlantoes()
    {
        var responce = await _service.Consultar();
        return Ok(responce);
    }

    [HttpGet("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlantaoId(long id)
    {
        var responce = await _service.ConsultarId(id);
        if (responce != null)
        {
            return Ok(responce);
        }
        return BadRequest("Plantao não existente ou não encontrado");
    }

    [HttpPost("plantoes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostPlantao([FromBody]  RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Cadastrar(plantao);
        if (response)
        {
            return Created("Plantao adcionado", plantao);
        }
        return BadRequest("Não foi possivel criar esse plantao");
    }

    [HttpPut("plantoes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutPlantoes([FromBody] RequestPlantaoRegisterJson plantao)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _service.Editar(plantao);
        if (response)
        {
            return Created("Plantao editado", plantao);
        }
        return BadRequest("Não foi possivel alterar esse plantao");
    }

    [HttpDelete("plantoes/{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePlantoes(long id)
    {
        var responce = await _service.Deletar(id);
        if (responce != null)
        {
            return Accepted("Plantao deletado com sucesso", responce);
        }
        return BadRequest("Não foi possivel deletar esse plantao");
    }
    
}