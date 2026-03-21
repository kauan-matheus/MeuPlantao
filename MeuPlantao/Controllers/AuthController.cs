using MeuPlantao.Application.Services.Auth;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/auth/login")]
    [AllowAnonymous] // Login precisa ficar público para emitir o primeiro JWT
    [ProducesResponseType(typeof(ResponseAuthLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] RequestAuthLoginJson auth)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _authService.Login(auth);

        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("/auth/register")]
    [AllowAnonymous] // Registro padrão é público e cria usuário profissional
    [ProducesResponseType(typeof(ResponseAuthRegisterJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestAuthRegisterJson request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _authService.Register(request);

        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }

    [HttpPost("/auth/register-admin")]
    [Authorize(Roles = nameof(RoleEnum.Admin))] // Apenas admins podem criar novos admins
    [ProducesResponseType(typeof(ResponseAuthRegisterJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RegisterAdmin([FromBody] RequestAuthRegisterAdminJson request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _authService.RegisterAdmin(request);

        if (response.Success)
            return StatusCode(response.StatusCode, response.Data);

        return StatusCode(response.StatusCode, response.Message);
    }
}