using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Communication.Enums;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.Profissional
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfRepository _repository;
        private readonly IUnitOfWork _unit;

        public ProfissionalService(IProfRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ServiceResponse<List<ProfissionalModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<ProfissionalModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<ProfissionalModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ProfissionalModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ProfissionalModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<ProfissionalModel>(id);

                if (data is null)
                    return ServiceResponse<ProfissionalModel>.BadRequest("Registro não encontrado");

                return ServiceResponse<ProfissionalModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProfissionalModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ProfissionalModel>> ConsultarUserId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorUserId(id);

                if (data is null)
                    return ServiceResponse<ProfissionalModel>.BadRequest("Registro não encontrado");

                return ServiceResponse<ProfissionalModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProfissionalModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestProfissionalRegisterJson profissional)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
                if (existente is null)
                    return ServiceResponse<bool>.BadRequest("Usuário não encontrado");

                var novo = new ProfissionalModel
                {
                    Nome = profissional.Nome,
                    Role = profissional.Role,
                    Crm = profissional.Crm,
                    Coren = profissional.Crm,
                    Telefone = profissional.Telefone,
                    UserId = profissional.UserId,
                    User = existente,
                };

                await _repository.Cadastrar(novo);
                var result = await _unit.Commit();

                return ServiceResponse<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Editar(RequestProfissionalRegisterJson profissional)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<UserModel>(profissional.UserId);
                if (existente is null)
                    return ServiceResponse<bool>.BadRequest("Usuário não encontrado");

                var novo = new ProfissionalModel
                {
                    Id = profissional.Id,
                    Nome = profissional.Nome,
                    Role = profissional.Role,
                    Crm = profissional.Crm,
                    Coren = profissional.Crm,
                    Telefone = profissional.Telefone,
                    UserId = profissional.UserId,
                    User = existente,
                };

                await _repository.Editar(novo);
                var result = await _unit.Commit();

                return ServiceResponse<bool>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<ProfissionalModel>> Deletar(long id)
        {
            try
            {
                var existente = await ConsultarId(id);

                if (!existente.Success || existente.Data is null)
                    return ServiceResponse<ProfissionalModel>.BadRequest("Registro não encontrado");

                await _repository.Excluir(existente.Data);
                await _unit.Commit();

                return ServiceResponse<ProfissionalModel>.Ok(existente.Data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProfissionalModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<ResponsePlantaoJson>>> ConsultarPlantoes(long userLogado)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<UserModel>(userLogado);
                if (existente is null)
                    return ServiceResponse<List<ResponsePlantaoJson>>.BadRequest("Registro não encontrado");
                
                var profId = await _repository.ConsultarPorUserId(userLogado);
                if (profId is null)
                    return ServiceResponse<List<ResponsePlantaoJson>>.BadRequest("É necessario estar logado como um profissional");
                
                var query = _repository.ConsultarPlantoes(profId.Id);

                var plantoes = await query.Select(p => new ResponsePlantaoJson
                {
                    Id = p.Id,
                    Date = p.Inicio.ToString("dd/MM/yyyy"),
                    Start = p.Inicio.ToString("HH:mm"),
                    Duration = (p.Fim - p.Inicio).ToString(@"hh\:mm"),
                    Locale = p.Setor.Estabelecimento.Nome,
                    Sector = p.Setor.Nome,
                    Value = p.Valor,
                    Responsable = p.Setor.Representante.Email,
                    OnDuty = p.ProfissionalResponsavel != null 
                        ? p.ProfissionalResponsavel.Nome 
                        : "Disponivel"
                }).ToListAsync();

                return ServiceResponse<List<ResponsePlantaoJson>>.Ok(plantoes);
                
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ResponsePlantaoJson>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<ResponsePlantaoJson>>> ConsultarPlantoesSolicitados(long userLogado)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<UserModel>(userLogado);
                if (existente is null)
                    return ServiceResponse<List<ResponsePlantaoJson>>.BadRequest("Registro não encontrado");

                var profId = await _repository.ConsultarPorUserId(userLogado);
                if (profId is null)
                    return ServiceResponse<List<ResponsePlantaoJson>>.BadRequest("É necessario estar logado como um profissional");
                
                var query = _repository.ConsultarPlantoesSolicitados(profId.Id);

                var plantoes = await query.Select(p => new ResponsePlantaoJson
                {
                    Id = p.Id,
                    Date = p.Inicio.ToString("dd/MM/yyyy"),
                    Start = p.Inicio.ToString("HH:mm"),
                    Duration = (p.Fim - p.Inicio).ToString(@"hh\:mm"),
                    Locale = p.Setor.Estabelecimento.Nome,
                    Sector = p.Setor.Nome,
                    Value = p.Valor,
                    Responsable = p.Setor.Representante.Email,
                    OnDuty = p.ProfissionalResponsavel != null 
                        ? p.ProfissionalResponsavel.Nome 
                        : "Disponivel"
                }).ToListAsync();

                return ServiceResponse<List<ResponsePlantaoJson>>.Ok(plantoes);
                
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ResponsePlantaoJson>>.Error(ex.Message);
            }
        }
    }
}