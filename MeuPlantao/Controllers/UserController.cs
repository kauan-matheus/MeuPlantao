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
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpGet("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserId(long id)
        {
            var response = await _service.ConsultarId(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
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
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
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
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("usuarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _service.Deletar(id);
            if (response.Success)
                return StatusCode(response.StatusCode, response.Data);

            return StatusCode(response.StatusCode, response.Message);
        }
    }
}