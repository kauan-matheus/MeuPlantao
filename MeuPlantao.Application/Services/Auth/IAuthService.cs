using MeuPlantao.Communication.Dto.Requests;
using MeuPlantao.Communication.Dto.Responses;

namespace MeuPlantao.Application.Services.Auth
{
public interface IAuthService
{
	Task<AuthServiceResponse<ResponseAuthLoginJson>> Login(RequestAuthLoginJson auth);
	Task<AuthServiceResponse<ResponseAuthRegisterJson>> Register(RequestAuthRegisterJson request);
	Task<AuthServiceResponse<ResponseAuthRegisterJson>> RegisterAdmin(RequestAuthRegisterAdminJson request);
}
}