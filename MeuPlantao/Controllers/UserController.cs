using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Application.Services.User;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Gestão de usuários é área administrativa.
    [Authorize(Roles = nameof(RoleEnum.Admin))]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser()
        {
            var responce = await _service.Consultar();
            return Ok(responce);
        }

        [HttpGet("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserId(long id)
        {
            var responce = await _service.ConsultarId(id);
            if (responce != null)
            {
                return Ok(responce);
            }
            return BadRequest("Usuario não existente ou não encontrado");
        }

        [HttpPost("usuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpPut("usuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.Editar(user);
            if (response)
            {
                return Created("Usuario editado", user);
            }
            return BadRequest("Não foi possivel alterar esse usuario");
        }

        [HttpDelete("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var responce = await _service.Deletar(id);
            if (responce != null)
            {
                return Accepted("Usuario deletado com sucesso", responce);
            }
            return BadRequest("Não foi possivel deletar esse usuario");
        }
    }
}