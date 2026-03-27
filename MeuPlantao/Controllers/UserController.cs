using MeuPlantao.Application.Services.User;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        // Interface em vez da classe concreta
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var response = await _service.Consultar();
            return Ok(response);
        }

        [HttpGet("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserId(long id)
        {
            var response = await _service.ConsultarId(id);
            if (response is not null)
                return Ok(response);

            return NotFound("Usuário não existente ou não encontrado");
        }

        [HttpPost("usuarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<IActionResult> PostUser([FromBody] RequestUserRegisterJson user)
        // RequestUserRegisterJson em vez de UserModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Cadastrar(user);
            if (response)
                return Created("Usuário adicionado", user);

            return BadRequest("Não foi possível criar esse usuário");
        }

        [HttpPut("usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)] // PUT retorna 200, não 201
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<IActionResult> PutUser([FromBody] RequestUserRegisterJson user)
        // RequestUserRegisterJson em vez de UserModel
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _service.Editar(user);
            if (response)
                return Ok(user);

            return BadRequest("Não foi possível alterar esse usuário");
        }

        [HttpDelete("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _service.Deletar(id);
            if (response is not null)
                return Ok(response);

            return NotFound("Não foi possível deletar esse usuário");
        }
    }
}