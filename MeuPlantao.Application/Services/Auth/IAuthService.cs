using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;

namespace MeuPlantao.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth);
        Task<ServiceResponse<ResponseAuthRegisterJson>> RegisterMedico(RequestAuthRegisterMedicoJson request);
        Task<ServiceResponse<ResponseAuthRegisterJson>> RegisterEnfermeiro(RequestAuthRegisterEnfermeiroJson request);
        Task<ServiceResponse<ResponseAuthRegisterJson>> RegisterAdmin(RequestAuthRegisterAdminJson request);
        Task<ServiceResponse<ResponseAuthRegisterJson>> RegisterGestor(RequestAuthRegisterGestorJson request);
    }
}