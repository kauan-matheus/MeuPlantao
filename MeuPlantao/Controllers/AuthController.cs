using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Data;
using MeuPlantao.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Controllers;

[ApiController]
[Route("api/[controller]/")]

public class AuthController : ControllerBase
{

    private readonly AppDbContext _appDbContext;

    public AuthController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost("/auth/login")]
    public async Task<IActionResult> Login([FromBody] RequestAuthLoginJson auth)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //FirstOrDefaultAsync da o primeiro registro que satisfaça essa condição se nn encontrar retorna NULL.
        var usuario = await _appDbContext.Usuarios
            .FirstOrDefaultAsync(usuario => usuario.Email == auth.Email);

        if (usuario == null)
            return Unauthorized("Email ou senha inválidos");

        if (usuario.PasswordHash != auth.Password)
            return Unauthorized("Email ou senha inválidos");

        return Ok("Login realizado com sucesso");
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

        return Ok("Usuário registrado com sucesso");
    }

}