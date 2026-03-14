using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Infrastructure.Data;
using MeuPlantao.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeuPlantao.Application.Services;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]/")]

public class AuthController : ControllerBase
{

    private readonly AppDbContext _appDbContext;
    private readonly TokenService _tokenService;

    public AuthController(AppDbContext appDbContext, TokenService tokenService)
    {
        _appDbContext = appDbContext;
        _tokenService = tokenService;
    }

    [HttpPost("/auth/login")]
    public async Task<IActionResult> Login([FromBody] RequestAuthLoginJson auth)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //FirstOrDefaultAsync da o primeiro registro que satisfaça essa condição se nn encontrar retorna NULL.
        var usuario = await _appDbContext.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Email == auth.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(auth.Password, usuario.PasswordHash))
            return Unauthorized("Email ou senha inválidos");

        if (!usuario.Active)
            return Unauthorized("Usuário inativo");

        var token = _tokenService.GenerateToken(usuario);

        return Ok(new
        {
            token,
            expiresIn = "8h",
            usuario = new {usuario.Id, usuario.Email, role = usuario.Role.ToString()}
        });
    }

    [HttpPost("/auth/register")]
    public async Task<IActionResult> Register([FromBody] RequestAuthRegisterJson request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var emailExiste = await _appDbContext.Usuarios
            .AnyAsync(u => u.Email == request.Email);

        if (emailExiste)
            return BadRequest("Email já cadastrado");

        //BCrypt.Net → namespace; BCrypt → classe; HashPassword() → método
        //O método HashPassword da classe BCrypt que está dentro do namespace BCrypt.Net.
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var usuario = new UserModel
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = RoleEnum.Profissional,
            Active = true
        };

        _appDbContext.Usuarios.Add(usuario);
        await _appDbContext.SaveChangesAsync();

        var profissional = new ProfissionalModel
        {
            Nome = request.Nome,
            Crm = request.Crm,
            Telefone = request.Telefone,
            UserId = usuario.Id
        };

        _appDbContext.Profissionais.Add(profissional);
        await _appDbContext.SaveChangesAsync();

        var token = _tokenService.GenerateToken(usuario);

        return Ok(new
        {
            messge = "Usuario registrado com sucesso",
            token,
            usuario = new {usuario.Id, usuario.Email}
        });
    }

}