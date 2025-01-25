using Core.V1.Login.Models;

namespace Core.V1.Login.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Autenticacao(string username, string password);
    }
}
