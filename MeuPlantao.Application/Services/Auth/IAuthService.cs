using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;

namespace MeuPlantao.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth);
        Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterMedico(RequestAuthRegisterMedicoJson request);
        Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterEnfermeiro(RequestAuthRegisterEnfermeiroJson request);
        Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterAdmin(RequestAuthRegisterAdminJson request);
        Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterGestor(RequestAuthRegisterGestorJson request);
    }
}