using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Application.Services.PlantaoHistorico;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistoricoPlantaoController : ControllerBase
    {
        private readonly IPlantaoHistoricoService _service;

        public HistoricoPlantaoController(IPlantaoHistoricoService service)
        {
            _service = service;
        }

        [HttpGet("plantoesHistorico")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlantoes()
        {
            var response = await _service.Consultar();
            return Ok(response);
        }
    }
}