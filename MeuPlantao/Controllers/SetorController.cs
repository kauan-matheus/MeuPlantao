using MeuPlantao.Application.Services.Setor;
using MeuPlantao.Communication.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetorController : ControllerBase
{
    // Injeta a interface, não a classe concreta — segue Clean Architecture
    private readonly ISetorService _service;

    public SetorController(ISetorService service)
    {
        _service = service;
    }

    [HttpGet("setores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSetores()
    {
        var response = await _service.Consultar();
        return Ok(response);
    }

    [HttpGet("setores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado, não 400
    public async Task<IActionResult> GetSetorId(long id)
    {
        var response = await _service.ConsultarId(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Setor não existente ou não encontrado");
    }

    [HttpPost("setores")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostSetores([FromBody] RequestSetorRegisterJson setor)
    // RequestSetorRegisterJson em vez de SetorModel — nunca expõe entidade de domínio na API
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Cadastrar(setor);
        if (response)
            return Created("Setor adicionado", setor);

        return BadRequest("Não foi possível criar esse setor");
    }

    [HttpPut("setores")]
    [ProducesResponseType(StatusCodes.Status200OK)] // PUT bem-sucedido retorna 200, não 201
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutSetores([FromBody] RequestSetorRegisterJson setor)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _service.Editar(setor);
        if (response)
            return Ok(setor);

        return BadRequest("Não foi possível alterar esse setor");
    }

    [HttpDelete("setores/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404 para recurso não encontrado
    public async Task<IActionResult> DeleteSetores(long id)
    {
        var response = await _service.Deletar(id);
        if (response is not null)
            return Ok(response);

        return NotFound("Não foi possível deletar esse setor");
    }
}