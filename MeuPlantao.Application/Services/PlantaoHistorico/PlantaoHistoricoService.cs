using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;
using MeuPlantao.Domain.Entities;
using MeuPlantao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuPlantao.Application.Services.PlantaoHistorico
{
    public class PlantaoHistoricoService : IPlantaoHistoricoService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unit;

        public PlantaoHistoricoService(IRepository repository, IUnitOfWork unit)
        {
            _repository = repository;
            _unit = unit;
        }

        public async Task<ServiceResponse<List<PlantaoHistoricoModel>>> Consultar()
        {
            try
            {
                var data = await _repository.Consultar<PlantaoHistoricoModel>()
                    .OrderBy(p => p.Id)
                    .ToListAsync();

                return ServiceResponse<List<PlantaoHistoricoModel>>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<PlantaoHistoricoModel>>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<PlantaoHistoricoModel>> ConsultarId(long id)
        {
            try
            {
                var data = await _repository.ConsultarPorId<PlantaoHistoricoModel>(id);

                if (data is null)
                    return ServiceResponse<PlantaoHistoricoModel>.BadRequest("Registro não encontrado");

                return ServiceResponse<PlantaoHistoricoModel>.Ok(data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PlantaoHistoricoModel>.Error(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> Cadastrar(RequestPlantaoHistoricoRegisterJson plantao)
        {
            try
            {
                var novo = new PlantaoHistoricoModel
                {
                    PlantaoId = plantao.PlantaoId,
                    Evento = plantao.Evento,
                    UsuarioId = plantao.UsuarioId,
                    Observacao = plantao.Observacao
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

        public async Task<ServiceResponse<bool>> Editar(RequestPlantaoHistoricoRegisterJson plantao)
        {
            try
            {
                var novo = new PlantaoHistoricoModel
                {
                    Id = plantao.Id,
                    PlantaoId = plantao.PlantaoId,
                    Evento = plantao.Evento,
                    UsuarioId = plantao.UsuarioId,
                    Observacao = plantao.Observacao
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

        public async Task<ServiceResponse<PlantaoHistoricoModel>> Deletar(long id)
        {
            try
            {
                var existente = await ConsultarId(id);

                if (!existente.Success || existente.Data is null)
                    return ServiceResponse<PlantaoHistoricoModel>.BadRequest("Registro não encontrado");

                await _repository.Excluir(existente.Data);
                await _unit.Commit();

                return ServiceResponse<PlantaoHistoricoModel>.Ok(existente.Data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PlantaoHistoricoModel>.Error(ex.Message);
            }
        }
    }
}