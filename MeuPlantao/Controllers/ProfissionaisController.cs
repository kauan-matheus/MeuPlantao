using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Application.Services;
using MeuPlantao.Domain.Entities;

namespace MeuPlantao.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProfissionaisController : ControllerBase
{
    private readonly ProfissionalService _service;

    public ProfissionaisController(ProfissionalService service)
    {
        _service = service;
    }

    [HttpGet("profissionais")]
    public IActionResult GetProfissionais()
    {
        var responce = _service.Consultar();
        return Ok(responce);
    }

    [HttpPost("profissionais")]
    public IActionResult PostProfissional([FromBody]  RequestProfissionalRegisterJson profissional)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = _service.Cadastrar(profissional);

        return Created("Profissional adcionado", profissional);
    }
    
}