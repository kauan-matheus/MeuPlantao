using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Application.Services;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Application.Services.User;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("profissionais")]
        public async Task<IActionResult> GetProfissionais()
        {
            var responce = await _service.Consultar();
            return Ok(responce);
        }

        [HttpPost("profissionais")]
        public async Task<IActionResult> PostUser([FromBody]  UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.Cadastrar(user);
            if (response)
            {
                return Created("Usuario adcionado", user);
            }
            return BadRequest("Não foi possivel criar esse usuario");
        }
    }
}