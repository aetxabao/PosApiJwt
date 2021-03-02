
using PosApiJwt.Requests;
using PosApiJwt.Responses;
using System.Threading.Tasks;

namespace PosApiJwt.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
