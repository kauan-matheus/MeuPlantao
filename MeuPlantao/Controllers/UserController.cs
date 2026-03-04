using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Application.Services;
using MeuPlantao.Domain.Entities;

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
        public IActionResult GetProfissionais()
        {
            var responce = _service.Consultar();
            return Ok(responce);
        }

        [HttpPost("profissionais")]
        public IActionResult PostUser([FromBody]  UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _service.Cadastrar(user);

            return Created("user adcionado", user);
        }
    }
}