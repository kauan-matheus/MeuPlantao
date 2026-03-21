using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace MeuPlantao.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly TokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    // ILogger injetado para registrar erros sem silenciá-los
    public AuthService(IAuthRepository repository, TokenService tokenService, ILogger<AuthService> logger)
    {
        _repository = repository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<AuthServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth)
    {
        var usuario = await _repository.ConsultarUsuarioPorEmail(auth.Email);

        // Roda o BCrypt em background para não bloquear o fluxo principal
        // Task.Run retorna false se o usuário não existir, evitando exception
        var senhaValida = usuario != null && await Task.Run(() => BCrypt.Net.BCrypt.Verify(auth.Password, usuario.PasswordHash));

        // Mesma mensagem para email inexistente e senha errada (segurança)
        if (usuario == null || !senhaValida)
            return AuthServiceResponse<ResponseAuthLoginJson>.Unauthorized("Email ou senha inválidos");

        if (!usuario.Active)
            return AuthServiceResponse<ResponseAuthLoginJson>.Unauthorized("Usuário inativo");

        // gerar o token do JWT
        var token = _tokenService.GenerateToken(usuario);

        return AuthServiceResponse<ResponseAuthLoginJson>.Ok(new ResponseAuthLoginJson
        {
            Token = token,
            ExpiresIn = "8h",
            Usuario = new ResponseAuthUserJson
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Role = usuario.Role.ToString()
            }
        });
    }

    public async Task<AuthServiceResponse<ResponseAuthRegisterJson>> Register(RequestAuthRegisterJson request)
    {
        var emailExiste = await _repository.ExisteUsuarioPorEmail(request.Email);
        if (emailExiste)
            return AuthServiceResponse<ResponseAuthRegisterJson>.BadRequest("Email já cadastrado");

        // BCrypt em background — HashPassword é pesado por design (segurança)
        var passwordHash = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(request.Password));

        var usuario = new UserModel
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = RoleEnum.Profissional,
            Active = true
        };

        var profissional = new ProfissionalModel
        {
            Nome = request.Nome,
            Crm = request.Crm,
            Telefone = request.Telefone
        };

        try
        {
            // Cadastra usuário e profissional dentro de uma transaction (atômico)
            await _repository.CadastrarUsuarioComProfissional(usuario, profissional);
        }
        catch (Exception ex)
        {
            // Loga o erro real para debug, mas retorna mensagem genérica ao cliente
            _logger.LogError(ex, "Erro ao registrar usuário: {Email}", request.Email);
            return AuthServiceResponse<ResponseAuthRegisterJson>.Error("Não foi possível registrar o usuário");
        }

        // Após o cadastro o EF Core já preencheu usuario.Id com o valor gerado pelo banco
        // Só geramos o token aqui para garantir que o Id é válido
        var token = _tokenService.GenerateToken(usuario);

        return AuthServiceResponse<ResponseAuthRegisterJson>.Ok(new ResponseAuthRegisterJson
        {
            Message = "Usuário registrado com sucesso",
            Token = token,
            Usuario = new ResponseAuthUserJson
            {
                Id = usuario.Id,   // Id já populado pelo EF Core
                Email = usuario.Email
            }
        });
    }

    public async Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterAdmin(RequestAuthRegisterAdminJson request)
    {
        var emailExiste = await _repository.ExisteUsuarioPorEmail(request.Email);
        if (emailExiste)
            return AuthServiceResponse<ResponseAuthRegisterJson>.BadRequest("Email já cadastrado");

        // BCrypt em background — mesma razão do Register
        var passwordHash = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(request.Password));

        // Admin usa a mesma entidade User, a diferença de acesso é definida pelo Role
        var usuario = new UserModel
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = RoleEnum.Admin,
            Active = true
        };

        try
        {
            await _repository.Cadastrar(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar admin: {Email}", request.Email);
            return AuthServiceResponse<ResponseAuthRegisterJson>.Error("Não foi possível registrar o admin");
        }

        // Token gerado após cadastro para garantir que usuario.Id foi populado
        var token = _tokenService.GenerateToken(usuario);

        return AuthServiceResponse<ResponseAuthRegisterJson>.Ok(new ResponseAuthRegisterJson
        {
            Message = "Admin registrado com sucesso",
            Token = token,
            Usuario = new ResponseAuthUserJson
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Role = usuario.Role.ToString()
            }
        });
    }
}