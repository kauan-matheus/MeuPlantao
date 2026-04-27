using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.TrocaHistorico
{
    public class TrocaHistoricoService : ITrocaHistoricoService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public TrocaHistoricoService(IRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ServiceResponse<List<TrocaHistoricoModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<TrocaHistoricoModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<TrocaHistoricoModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<TrocaHistoricoModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<TrocaHistoricoModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<TrocaHistoricoModel>(id);

                if (data is null)
                    return ServiceResponse<TrocaHistoricoModel>.BadRequest("Registro não encontrado");

                return ServiceResponse<TrocaHistoricoModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrocaHistoricoModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestTrocaHistoricoRegisterJson troca)
        {
            try
            {
                var novo = new TrocaHistoricoModel
                {
                    TrocaPlantaoId = troca.TrocaPlantaoId,
                    Evento = troca.Evento,
                    UsuarioId = troca.UsuarioId,
                    Observacao = troca.Observacao
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

        public async Task<ServiceResponse<bool>> Editar(RequestTrocaHistoricoRegisterJson troca)
        {
            try
            {
                var novo = new TrocaHistoricoModel
                {
                    Id = troca.Id,
                    TrocaPlantaoId = troca.TrocaPlantaoId,
                    Evento = troca.Evento,
                    UsuarioId = troca.UsuarioId,
                    Observacao = troca.Observacao
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

        public async Task<ServiceResponse<TrocaHistoricoModel>> Deletar(long id)
        {
            try
            {
                var existente = await _repository.ConsultarPorId<TrocaHistoricoModel>(id);

                if (existente is null)
                    return ServiceResponse<TrocaHistoricoModel>.BadRequest("Registro não encontrado");

                await _repository.Excluir(existente);
                await _unit.Commit();

                return ServiceResponse<TrocaHistoricoModel>.Ok(existente);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TrocaHistoricoModel>.Error(ex.Message);
            }
        }
    }
}