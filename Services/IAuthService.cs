using Fiap.Api.MonitoramentoAmbiental.Model;

namespace Fiap.Api.MonitoramentoAmbiental.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password);

    }

} //FIM

