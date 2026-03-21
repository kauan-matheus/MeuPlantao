using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;

namespace MeuPlantao.Application.Services.Auth;

public class AuthService : IAuthService
{
	private readonly IRepository _repository;
	private readonly TokenService _tokenService;

	public AuthService(IRepository repository, TokenService tokenService)
	{
		_repository = repository;
		_tokenService = tokenService;
	}

	public async Task<AuthServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth)
	{
		var usuario = await _repository.ConsultarUsuarioPorEmail(auth.Email);

		if (usuario == null || !BCrypt.Net.BCrypt.Verify(auth.Password, usuario.PasswordHash))
			return AuthServiceResponse<ResponseAuthLoginJson>.Unauthorized("Email ou senha inválidos");

		if (!usuario.Active)
			return AuthServiceResponse<ResponseAuthLoginJson>.Unauthorized("Usuário inativo");

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

		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

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

		var cadastrado = await _repository.CadastrarUsuarioComProfissional(usuario, profissional);
		if (!cadastrado)
			return AuthServiceResponse<ResponseAuthRegisterJson>.Error("Não foi possível registrar o usuário");

		var token = _tokenService.GenerateToken(usuario);

		return AuthServiceResponse<ResponseAuthRegisterJson>.Ok(new ResponseAuthRegisterJson
		{
			Message = "Usuario registrado com sucesso",
			Token = token,
			Usuario = new ResponseAuthUserJson
			{
				Id = usuario.Id,
				Email = usuario.Email
			}
		});
	}

	public async Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterAdmin(RequestAuthRegisterAdminJson request)
	{
		var emailExiste = await _repository.ExisteUsuarioPorEmail(request.Email);
		if (emailExiste)
			return AuthServiceResponse<ResponseAuthRegisterJson>.BadRequest("Email já cadastrado");

		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

		// Admin usa a mesma entidade User. A diferença de acesso é definida pelo Role.
		var usuario = new UserModel
		{
			Email = request.Email,
			PasswordHash = passwordHash,
			Role = RoleEnum.Admin,
			Active = true
		};

		var cadastrado = await _repository.Cadastrar(usuario);
		if (!cadastrado)
			return AuthServiceResponse<ResponseAuthRegisterJson>.Error("Não foi possível registrar o admin");

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
