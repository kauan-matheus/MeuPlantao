using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Application.Services.TrocaHistorico;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Todos os endpoints exigem autenticação por padrão
    public class HistoricoTrocasController : ControllerBase
    {
        private readonly ITrocaHistoricoService _service;

        public HistoricoTrocasController(ITrocaHistoricoService service)
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
    }
}